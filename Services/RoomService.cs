using Movie_Reservation_System.DTOs.Room;
using Movie_Reservation_System.Query;
using Movie_Reservation_System.Models;
using Movie_Reservation_System.Repositories;
using Movie_Reservation_System.Helpers;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Reservation_System.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repo;
        private readonly IValidator<RoomCreateDto> _createValidator;
        private readonly IValidator<RoomUpdateDto> _updateValidator;
        private readonly Data.ApplicationDbContext _db;

        public RoomService(IRoomRepository repo, IValidator<RoomCreateDto> createValidator, IValidator<RoomUpdateDto> updateValidator, Data.ApplicationDbContext db)
        {
            _repo = repo;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _db = db;
        }

        public async Task<Result<int>> CreateAsync(RoomCreateDto dto)
        {
            var validation = await _createValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Result<int>.Fail(validation.Errors.Select(e => e.ErrorMessage).ToList());

            var room = new Room { Name = dto.Name };
            await _repo.AddAsync(room);
            await _db.SaveChangesAsync();
            return Result<int>.Ok(room.Id, "Sala creada correctamente.");
        }

        public async Task<Result<PagedResult<Room>>> GetAllAsync(RoomQueryDto query)
        {
            try
            {
                var result = await _repo.GetAllAsync(query);
                return Result<PagedResult<Room>>.Ok(result);
            }
            catch (System.Exception ex)
            {
                return Result<PagedResult<Room>>.Fail($"Error al obtener salas: {ex.Message}", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result<Room?>> GetByIdAsync(int id)
        {
            var room = await _repo.GetByIdAsync(id);
            if (room == null)
                return Result<Room?>.Fail("Sala no encontrada.", System.Net.HttpStatusCode.NotFound);
            return Result<Room?>.Ok(room);
        }

        public async Task<Result<bool>> UpdateAsync(RoomUpdateDto dto)
        {
            var validation = await _updateValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Result<bool>.Fail(validation.Errors.Select(e => e.ErrorMessage).ToList());

            var room = await _repo.GetByIdAsync(dto.Id);
            if (room == null)
                return Result<bool>.Fail("Sala no encontrada.", System.Net.HttpStatusCode.NotFound);

            room.Name = dto.Name;
            await _repo.UpdateAsync(room);
            await _db.SaveChangesAsync();
            return Result<bool>.Ok(true, "Sala actualizada correctamente.");
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var room = await _repo.GetByIdAsync(id);
            if (room == null)
                return Result<bool>.Fail("Sala no encontrada.", System.Net.HttpStatusCode.NotFound);
            await _repo.DeleteAsync(room);
            await _db.SaveChangesAsync();
            return Result<bool>.Ok(true, "Sala eliminada correctamente.");
        }
    }
} 