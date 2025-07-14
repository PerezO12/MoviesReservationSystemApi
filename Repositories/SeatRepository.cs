using Movie_Reservation_System.Models;
using Movie_Reservation_System.Data;
using Microsoft.EntityFrameworkCore;
using Movie_Reservation_System.Query;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Movie_Reservation_System.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly ApplicationDbContext _db;
        public SeatRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Seat?> GetByIdAsync(int id)
            => await _db.Seats.FindAsync(id);

        public async Task<PagedResult<Seat>> GetAllAsync(SeatQueryDto query)
        {
            var seats = _db.Seats.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Row))
                seats = seats.Where(s => s.Row.Contains(query.Row));
            if (query.Number.HasValue)
                seats = seats.Where(s => s.Number == query.Number.Value);
            if (query.RoomId.HasValue)
                seats = seats.Where(s => s.RoomId == query.RoomId.Value);

            if (!string.IsNullOrWhiteSpace(query.OrderBy))
            {
                if (query.OrderBy.ToLower() == "row")
                    seats = query.Desc ? seats.OrderByDescending(s => s.Row) : seats.OrderBy(s => s.Row);
                else if (query.OrderBy.ToLower() == "number")
                    seats = query.Desc ? seats.OrderByDescending(s => s.Number) : seats.OrderBy(s => s.Number);
                else
                    seats = seats.OrderBy(s => s.Id);
            }
            else
            {
                seats = seats.OrderBy(s => s.Id);
            }

            var total = await seats.CountAsync();
            var items = await seats.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize).ToListAsync();

            return new PagedResult<Seat>
            {
                Items = items,
                TotalCount = total,
                Page = query.Page,
                PageSize = query.PageSize
            };
        }

        public async Task AddAsync(Seat seat)
        {
            await _db.Seats.AddAsync(seat);
        }

        public async Task UpdateAsync(Seat seat)
        {
            _db.Seats.Update(seat);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Seat seat)
        {
            _db.Seats.Remove(seat);
            await Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(int id)
            => await _db.Seats.AnyAsync(s => s.Id == id);
    }
} 