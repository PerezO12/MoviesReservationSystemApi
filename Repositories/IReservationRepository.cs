using Movie_Reservation_System.Models;
using Movie_Reservation_System.Query;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie_Reservation_System.Repositories
{
    public interface IReservationRepository
    {
        Task<Reservation?> GetByIdAsync(int id);
        Task<PagedResult<Reservation>> GetAllAsync(ReservationQueryDto query);
        Task AddAsync(Reservation reservation);
        Task UpdateAsync(Reservation reservation);
        Task DeleteAsync(Reservation reservation);
        Task<bool> ExistsAsync(int id);
    }
} 