using Movie_Reservation_System.DTOs.Seat;
using Movie_Reservation_System.Query;
using Movie_Reservation_System.Models;
using Movie_Reservation_System.Repositories;
using Movie_Reservation_System.Helpers;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Reservation_System.Services
{
    public class SeatService : ISeatService
    {
        private readonly ISeatRepository _repo;
        private readonly IValidator<SeatCreateDto> _createValidator;
        private readonly IValidator<SeatUpdateDto> _updateValidator;
        private readonly Data.ApplicationDbContext _db;

        public SeatService(ISeatRepository repo, IValidator<SeatCreateDto> createValidator, IValidator<SeatUpdateDto> updateValidator, Data.ApplicationDbContext db)
        {
            _repo = repo;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _db = db;
        }

        public async Task<Result<int>> CreateAsync(SeatCreateDto dto)
        {
            var validation = await _createValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Result<int>.Fail(validation.Errors.Select(e => e.ErrorMessage).ToList());

            var seat = new Seat { Row = dto.Row, Number = dto.Number, RoomId = dto.RoomId };
            await _repo.AddAsync(seat);
            await _db.SaveChangesAsync();
            return Result<int>.Ok(seat.Id, "Asiento creado correctamente.");
        }

        public async Task<Result<PagedResult<Seat>>> GetAllAsync(SeatQueryDto query)
        {
            try
            {
                var result = await _repo.GetAllAsync(query);
                return Result<PagedResult<Seat>>.Ok(result);
            }
            catch (System.Exception ex)
            {
                return Result<PagedResult<Seat>>.Fail($"Error al obtener asientos: {ex.Message}", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result<Seat?>> GetByIdAsync(int id)
        {
            var seat = await _repo.GetByIdAsync(id);
            if (seat == null)
                return Result<Seat?>.Fail("Asiento no encontrado.", System.Net.HttpStatusCode.NotFound);
            return Result<Seat?>.Ok(seat);
        }

        public async Task<Result<bool>> UpdateAsync(SeatUpdateDto dto)
        {
            var validation = await _updateValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Result<bool>.Fail(validation.Errors.Select(e => e.ErrorMessage).ToList());

            var seat = await _repo.GetByIdAsync(dto.Id);
            if (seat == null)
                return Result<bool>.Fail("Asiento no encontrado.", System.Net.HttpStatusCode.NotFound);

            seat.Row = dto.Row;
            seat.Number = dto.Number;
            seat.RoomId = dto.RoomId;
            await _repo.UpdateAsync(seat);
            await _db.SaveChangesAsync();
            return Result<bool>.Ok(true, "Asiento actualizado correctamente.");
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var seat = await _repo.GetByIdAsync(id);
            if (seat == null)
                return Result<bool>.Fail("Asiento no encontrado.", System.Net.HttpStatusCode.NotFound);
            await _repo.DeleteAsync(seat);
            await _db.SaveChangesAsync();
            return Result<bool>.Ok(true, "Asiento eliminado correctamente.");
        }
    }
} 