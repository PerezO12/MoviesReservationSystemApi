using Movie_Reservation_System.Models;
using Movie_Reservation_System.Query;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie_Reservation_System.Repositories
{
    public interface IShowtimeRepository
    {
        Task<Showtime?> GetByIdAsync(int id);
        Task<PagedResult<Showtime>> GetAllAsync(ShowtimeQueryDto query);
        Task AddAsync(Showtime showtime);
        Task UpdateAsync(Showtime showtime);
        Task DeleteAsync(Showtime showtime);
        Task<bool> ExistsAsync(int id);
    }
} 