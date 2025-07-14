using Movie_Reservation_System.Models;
using Movie_Reservation_System.Data;
using Microsoft.EntityFrameworkCore;
using Movie_Reservation_System.Query;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Movie_Reservation_System.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _db;
        public ReservationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Reservation?> GetByIdAsync(int id)
            => await _db.Reservations.Include(r => r.ReservationSeats).FirstOrDefaultAsync(r => r.Id == id);

        public async Task<PagedResult<Reservation>> GetAllAsync(ReservationQueryDto query)
        {
            var reservations = _db.Reservations.Include(r => r.ReservationSeats).AsQueryable();
            if (query.UserId.HasValue)
                reservations = reservations.Where(r => r.UserId == query.UserId.ToString());
            if (query.ShowtimeId.HasValue)
                reservations = reservations.Where(r => r.ShowtimeId == query.ShowtimeId.Value);
            if (!string.IsNullOrWhiteSpace(query.Status))
                reservations = reservations.Where(r => r.Status == query.Status);

            if (!string.IsNullOrWhiteSpace(query.OrderBy))
            {
                if (query.OrderBy.ToLower() == "createdat")
                    reservations = query.Desc ? reservations.OrderByDescending(r => r.CreatedAt) : reservations.OrderBy(r => r.CreatedAt);
                else
                    reservations = reservations.OrderBy(r => r.Id);
            }
            else
            {
                reservations = reservations.OrderBy(r => r.Id);
            }

            var total = await reservations.CountAsync();
            var items = await reservations.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize).ToListAsync();

            return new PagedResult<Reservation>
            {
                Items = items,
                TotalCount = total,
                Page = query.Page,
                PageSize = query.PageSize
            };
        }

        public async Task AddAsync(Reservation reservation)
        {
            await _db.Reservations.AddAsync(reservation);
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            _db.Reservations.Update(reservation);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Reservation reservation)
        {
            _db.Reservations.Remove(reservation);
            await Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(int id)
            => await _db.Reservations.AnyAsync(r => r.Id == id);
    }
} 