using Movie_Reservation_System.Models;
using Movie_Reservation_System.Query;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie_Reservation_System.Repositories
{
    public interface IRoomRepository
    {
        Task<Room?> GetByIdAsync(int id);
        Task<PagedResult<Room>> GetAllAsync(RoomQueryDto query);
        Task AddAsync(Room room);
        Task UpdateAsync(Room room);
        Task DeleteAsync(Room room);
        Task<bool> ExistsAsync(int id);
    }
} 