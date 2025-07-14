using Movie_Reservation_System.Models;
using Movie_Reservation_System.Data;
using Microsoft.EntityFrameworkCore;
using Movie_Reservation_System.Query;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Movie_Reservation_System.Repositories
{
    public class ShowtimeRepository : IShowtimeRepository
    {
        private readonly ApplicationDbContext _db;
        public ShowtimeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Showtime?> GetByIdAsync(int id)
            => await _db.Showtimes.FindAsync(id);

        public async Task<PagedResult<Showtime>> GetAllAsync(ShowtimeQueryDto query)
        {
            var showtimes = _db.Showtimes.AsQueryable();
            if (query.MovieId.HasValue)
                showtimes = showtimes.Where(s => s.MovieId == query.MovieId.Value);
            if (query.RoomId.HasValue)
                showtimes = showtimes.Where(s => s.RoomId == query.RoomId.Value);
            if (query.Date.HasValue)
                showtimes = showtimes.Where(s => s.StartTime.Date == query.Date.Value.Date);

            if (!string.IsNullOrWhiteSpace(query.OrderBy))
            {
                if (query.OrderBy.ToLower() == "starttime")
                    showtimes = query.Desc ? showtimes.OrderByDescending(s => s.StartTime) : showtimes.OrderBy(s => s.StartTime);
                else
                    showtimes = showtimes.OrderBy(s => s.Id);
            }
            else
            {
                showtimes = showtimes.OrderBy(s => s.Id);
            }

            var total = await showtimes.CountAsync();
            var items = await showtimes.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize).ToListAsync();

            return new PagedResult<Showtime>
            {
                Items = items,
                TotalCount = total,
                Page = query.Page,
                PageSize = query.PageSize
            };
        }

        public async Task AddAsync(Showtime showtime)
        {
            await _db.Showtimes.AddAsync(showtime);
        }

        public async Task UpdateAsync(Showtime showtime)
        {
            _db.Showtimes.Update(showtime);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Showtime showtime)
        {
            _db.Showtimes.Remove(showtime);
            await Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(int id)
            => await _db.Showtimes.AnyAsync(s => s.Id == id);
    }
} 