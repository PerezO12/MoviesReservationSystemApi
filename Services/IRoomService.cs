using Movie_Reservation_System.DTOs.Room;
using Movie_Reservation_System.Query;
using Movie_Reservation_System.Models;
using Movie_Reservation_System.Helpers;
using System.Threading.Tasks;

namespace Movie_Reservation_System.Services
{
    public interface IRoomService
    {
        Task<Result<int>> CreateAsync(RoomCreateDto dto);
        Task<Result<PagedResult<Room>>> GetAllAsync(RoomQueryDto query);
        Task<Result<Room?>> GetByIdAsync(int id);
        Task<Result<bool>> UpdateAsync(RoomUpdateDto dto);
        Task<Result<bool>> DeleteAsync(int id);
    }
} 