using Movie_Reservation_System.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Movie_Reservation_System.Query;

namespace Movie_Reservation_System.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie?> GetByIdAsync(int id);
        Task<PagedResult<Movie>> GetAllAsync(MovieQueryDto query);
        Task AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(Movie movie);
        Task<bool> ExistsAsync(int id);
    }
} 