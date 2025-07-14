using Movie_Reservation_System.DTOs.Seat;
using Movie_Reservation_System.Query;
using Movie_Reservation_System.Models;
using Movie_Reservation_System.Helpers;
using System.Threading.Tasks;

namespace Movie_Reservation_System.Services
{
    public interface ISeatService
    {
        Task<Result<int>> CreateAsync(SeatCreateDto dto);
        Task<Result<PagedResult<Seat>>> GetAllAsync(SeatQueryDto query);
        Task<Result<Seat?>> GetByIdAsync(int id);
        Task<Result<bool>> UpdateAsync(SeatUpdateDto dto);
        Task<Result<bool>> DeleteAsync(int id);
    }
} 