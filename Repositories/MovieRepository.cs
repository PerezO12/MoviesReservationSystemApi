using Movie_Reservation_System.Models;
using Movie_Reservation_System.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Movie_Reservation_System.Query;

namespace Movie_Reservation_System.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _db;
        public MovieRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Movie?> GetByIdAsync(int id)
            => await _db.Movies.FindAsync(id);

        public async Task<PagedResult<Movie>> GetAllAsync(MovieQueryDto query)
        {
            var movies = _db.Movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Title))
                movies = movies.Where(m => m.Title.Contains(query.Title));
            if (!string.IsNullOrWhiteSpace(query.Genre))
                movies = movies.Where(m => m.Genre == query.Genre);

            // Ordenamiento
            if (!string.IsNullOrWhiteSpace(query.OrderBy))
            {
                if (query.OrderBy.ToLower() == "title")
                    movies = query.Desc ? movies.OrderByDescending(m => m.Title) : movies.OrderBy(m => m.Title);
                else if (query.OrderBy.ToLower() == "genre")
                    movies = query.Desc ? movies.OrderByDescending(m => m.Genre) : movies.OrderBy(m => m.Genre);
                else
                    movies = movies.OrderBy(m => m.Id);
            }
            else
            {
                movies = movies.OrderBy(m => m.Id);
            }

            var total = await movies.CountAsync();
            var items = await movies.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize).ToListAsync();

            return new PagedResult<Movie>
            {
                Items = items,
                TotalCount = total,
                Page = query.Page,
                PageSize = query.PageSize
            };
        }

        public async Task AddAsync(Movie movie)
        {
            await _db.Movies.AddAsync(movie);
        }

        public async Task UpdateAsync(Movie movie)
        {
            _db.Movies.Update(movie);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Movie movie)
        {
            _db.Movies.Remove(movie);
            await Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(int id)
            => await _db.Movies.AnyAsync(m => m.Id == id);
    }
} 