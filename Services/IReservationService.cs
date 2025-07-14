using Movie_Reservation_System.DTOs.Reservation;
using Movie_Reservation_System.Query;
using Movie_Reservation_System.Models;
using Movie_Reservation_System.Helpers;
using System.Threading.Tasks;

namespace Movie_Reservation_System.Services
{
    public interface IReservationService
    {
        Task<Result<int>> CreateAsync(ReservationCreateDto dto, string userId);
        Task<Result<PagedResult<Reservation>>> GetAllAsync(ReservationQueryDto query);
        Task<Result<Reservation?>> GetByIdAsync(int id);
        Task<Result<bool>> UpdateAsync(ReservationUpdateDto dto, string userId);
        Task<Result<bool>> DeleteAsync(int id, string userId);
    }
} 