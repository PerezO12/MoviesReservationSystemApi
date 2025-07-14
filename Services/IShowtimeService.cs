using Movie_Reservation_System.DTOs.Showtime;
using Movie_Reservation_System.Query;
using Movie_Reservation_System.Models;
using Movie_Reservation_System.Helpers;
using System.Threading.Tasks;

namespace Movie_Reservation_System.Services
{
    public interface IShowtimeService
    {
        Task<Result<int>> CreateAsync(ShowtimeCreateDto dto);
        Task<Result<PagedResult<Showtime>>> GetAllAsync(ShowtimeQueryDto query);
        Task<Result<Showtime?>> GetByIdAsync(int id);
        Task<Result<bool>> UpdateAsync(ShowtimeUpdateDto dto);
        Task<Result<bool>> DeleteAsync(int id);
    }
} 