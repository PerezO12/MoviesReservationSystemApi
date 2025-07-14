using Movie_Reservation_System.Models;
using Movie_Reservation_System.Data;
using Microsoft.EntityFrameworkCore;
using Movie_Reservation_System.Query;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Movie_Reservation_System.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _db;
        public RoomRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Room?> GetByIdAsync(int id)
            => await _db.Rooms.FindAsync(id);

        public async Task<PagedResult<Room>> GetAllAsync(RoomQueryDto query)
        {
            var rooms = _db.Rooms.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Name))
                rooms = rooms.Where(r => r.Name.Contains(query.Name));

            if (!string.IsNullOrWhiteSpace(query.OrderBy))
            {
                if (query.OrderBy.ToLower() == "name")
                    rooms = query.Desc ? rooms.OrderByDescending(r => r.Name) : rooms.OrderBy(r => r.Name);
                else
                    rooms = rooms.OrderBy(r => r.Id);
            }
            else
            {
                rooms = rooms.OrderBy(r => r.Id);
            }

            var total = await rooms.CountAsync();
            var items = await rooms.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize).ToListAsync();

            return new PagedResult<Room>
            {
                Items = items,
                TotalCount = total,
                Page = query.Page,
                PageSize = query.PageSize
            };
        }

        public async Task AddAsync(Room room)
        {
            await _db.Rooms.AddAsync(room);
        }

        public async Task UpdateAsync(Room room)
        {
            _db.Rooms.Update(room);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Room room)
        {
            _db.Rooms.Remove(room);
            await Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(int id)
            => await _db.Rooms.AnyAsync(r => r.Id == id);
    }
} 