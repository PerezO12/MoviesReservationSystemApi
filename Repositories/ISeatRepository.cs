using Movie_Reservation_System.Models;
using Movie_Reservation_System.Query;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie_Reservation_System.Repositories
{
    public interface ISeatRepository
    {
        Task<Seat?> GetByIdAsync(int id);
        Task<PagedResult<Seat>> GetAllAsync(SeatQueryDto query);
        Task AddAsync(Seat seat);
        Task UpdateAsync(Seat seat);
        Task DeleteAsync(Seat seat);
        Task<bool> ExistsAsync(int id);
    }
} 