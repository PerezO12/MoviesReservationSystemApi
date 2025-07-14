using Movie_Reservation_System.DTOs.Reservation;
using Movie_Reservation_System.Query;
using Movie_Reservation_System.Models;
using Movie_Reservation_System.Repositories;
using Movie_Reservation_System.Helpers;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Movie_Reservation_System.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repo;
        private readonly IValidator<ReservationCreateDto> _createValidator;
        private readonly IValidator<ReservationUpdateDto> _updateValidator;
        private readonly Data.ApplicationDbContext _db;

        public ReservationService(IReservationRepository repo, IValidator<ReservationCreateDto> createValidator, IValidator<ReservationUpdateDto> updateValidator, Data.ApplicationDbContext db)
        {
            _repo = repo;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _db = db;
        }

        public async Task<Result<int>> CreateAsync(ReservationCreateDto dto, string userId)
        {
            var validation = await _createValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Result<int>.Fail(validation.Errors.Select(e => e.ErrorMessage).ToList());

            // Validar que los asientos estén disponibles para el showtime
            var reservedSeats = await _db.ReservationSeats
                .Where(rs => rs.Reservation.ShowtimeId == dto.ShowtimeId && rs.Reservation.Status == "Active")
                .Select(rs => rs.SeatId)
                .ToListAsync();
            var unavailable = dto.SeatIds.Intersect(reservedSeats).ToList();
            if (unavailable.Any())
                return Result<int>.Fail($"Los asientos {string.Join(", ", unavailable)} ya están reservados para este horario.");

            var reservation = new Reservation
            {
                UserId = userId,
                ShowtimeId = dto.ShowtimeId,
                CreatedAt = System.DateTime.UtcNow,
                Status = "Active",
                ReservationSeats = dto.SeatIds.Select(seatId => new ReservationSeat { SeatId = seatId }).ToList()
            };
            await _repo.AddAsync(reservation);
            await _db.SaveChangesAsync();
            return Result<int>.Ok(reservation.Id, "Reserva creada correctamente.");
        }

        public async Task<Result<PagedResult<Reservation>>> GetAllAsync(ReservationQueryDto query)
        {
            try
            {
                var result = await _repo.GetAllAsync(query);
                return Result<PagedResult<Reservation>>.Ok(result);
            }
            catch (System.Exception ex)
            {
                return Result<PagedResult<Reservation>>.Fail($"Error al obtener reservas: {ex.Message}", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result<Reservation?>> GetByIdAsync(int id)
        {
            var reservation = await _repo.GetByIdAsync(id);
            if (reservation == null)
                return Result<Reservation?>.Fail("Reserva no encontrada.", System.Net.HttpStatusCode.NotFound);
            return Result<Reservation?>.Ok(reservation);
        }

        public async Task<Result<bool>> UpdateAsync(ReservationUpdateDto dto, string userId)
        {
            var validation = await _updateValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Result<bool>.Fail(validation.Errors.Select(e => e.ErrorMessage).ToList());

            var reservation = await _repo.GetByIdAsync(dto.Id);
            if (reservation == null)
                return Result<bool>.Fail("Reserva no encontrada.", System.Net.HttpStatusCode.NotFound);
            if (reservation.UserId != userId)
                return Result<bool>.Fail("No tienes permiso para modificar esta reserva.", System.Net.HttpStatusCode.Forbidden);
            if (reservation.Status != "Active")
                return Result<bool>.Fail("Solo se pueden modificar reservas activas.");

            // Validar que los nuevos asientos estén disponibles
            var reservedSeats = await _db.ReservationSeats
                .Where(rs => rs.Reservation.ShowtimeId == reservation.ShowtimeId && rs.Reservation.Status == "Active" && rs.ReservationId != reservation.Id)
                .Select(rs => rs.SeatId)
                .ToListAsync();
            var unavailable = dto.SeatIds.Intersect(reservedSeats).ToList();
            if (unavailable.Any())
                return Result<bool>.Fail($"Los asientos {string.Join(", ", unavailable)} ya están reservados para este horario.");

            // Actualizar asientos
            reservation.ReservationSeats = dto.SeatIds.Select(seatId => new ReservationSeat { ReservationId = reservation.Id, SeatId = seatId }).ToList();
            await _repo.UpdateAsync(reservation);
            await _db.SaveChangesAsync();
            return Result<bool>.Ok(true, "Reserva actualizada correctamente.");
        }

        public async Task<Result<bool>> DeleteAsync(int id, string userId)
        {
            var reservation = await _repo.GetByIdAsync(id);
            if (reservation == null)
                return Result<bool>.Fail("Reserva no encontrada.", System.Net.HttpStatusCode.NotFound);
            if (reservation.UserId != userId)
                return Result<bool>.Fail("No tienes permiso para cancelar esta reserva.", System.Net.HttpStatusCode.Forbidden);
            if (reservation.Status != "Active")
                return Result<bool>.Fail("Solo se pueden cancelar reservas activas.");

            reservation.Status = "Cancelled";
            await _repo.UpdateAsync(reservation);
            await _db.SaveChangesAsync();
            return Result<bool>.Ok(true, "Reserva cancelada correctamente.");
        }
    }
} 