using Movie_Reservation_System.DTOs.Showtime;
using Movie_Reservation_System.Query;
using Movie_Reservation_System.Models;
using Movie_Reservation_System.Repositories;
using Movie_Reservation_System.Helpers;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Reservation_System.Services
{
    public class ShowtimeService : IShowtimeService
    {
        private readonly IShowtimeRepository _repo;
        private readonly IValidator<ShowtimeCreateDto> _createValidator;
        private readonly IValidator<ShowtimeUpdateDto> _updateValidator;
        private readonly Data.ApplicationDbContext _db;

        public ShowtimeService(IShowtimeRepository repo, IValidator<ShowtimeCreateDto> createValidator, IValidator<ShowtimeUpdateDto> updateValidator, Data.ApplicationDbContext db)
        {
            _repo = repo;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _db = db;
        }

        public async Task<Result<int>> CreateAsync(ShowtimeCreateDto dto)
        {
            var validation = await _createValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Result<int>.Fail(validation.Errors.Select(e => e.ErrorMessage).ToList());

            var showtime = new Showtime { MovieId = dto.MovieId, RoomId = dto.RoomId, StartTime = dto.StartTime };
            await _repo.AddAsync(showtime);
            await _db.SaveChangesAsync();
            return Result<int>.Ok(showtime.Id, "Showtime creado correctamente.");
        }

        public async Task<Result<PagedResult<Showtime>>> GetAllAsync(ShowtimeQueryDto query)
        {
            try
            {
                var result = await _repo.GetAllAsync(query);
                return Result<PagedResult<Showtime>>.Ok(result);
            }
            catch (System.Exception ex)
            {
                return Result<PagedResult<Showtime>>.Fail($"Error al obtener showtimes: {ex.Message}", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result<Showtime?>> GetByIdAsync(int id)
        {
            var showtime = await _repo.GetByIdAsync(id);
            if (showtime == null)
                return Result<Showtime?>.Fail("Showtime no encontrado.", System.Net.HttpStatusCode.NotFound);
            return Result<Showtime?>.Ok(showtime);
        }

        public async Task<Result<bool>> UpdateAsync(ShowtimeUpdateDto dto)
        {
            var validation = await _updateValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Result<bool>.Fail(validation.Errors.Select(e => e.ErrorMessage).ToList());

            var showtime = await _repo.GetByIdAsync(dto.Id);
            if (showtime == null)
                return Result<bool>.Fail("Showtime no encontrado.", System.Net.HttpStatusCode.NotFound);

            showtime.MovieId = dto.MovieId;
            showtime.RoomId = dto.RoomId;
            showtime.StartTime = dto.StartTime;
            await _repo.UpdateAsync(showtime);
            await _db.SaveChangesAsync();
            return Result<bool>.Ok(true, "Showtime actualizado correctamente.");
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var showtime = await _repo.GetByIdAsync(id);
            if (showtime == null)
                return Result<bool>.Fail("Showtime no encontrado.", System.Net.HttpStatusCode.NotFound);
            await _repo.DeleteAsync(showtime);
            await _db.SaveChangesAsync();
            return Result<bool>.Ok(true, "Showtime eliminado correctamente.");
        }
    }
} 