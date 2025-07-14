using Microsoft.AspNetCore.Http;
using Movie_Reservation_System.DTOs.Movie;
using Movie_Reservation_System.Helpers;
using System.Threading.Tasks;
using Movie_Reservation_System.Query;
using Movie_Reservation_System.Models;

namespace Movie_Reservation_System.Services
{
    public interface IMovieService
    {
        /// <summary>
        /// Guarda el archivo de póster en el servidor y retorna la URL pública.
        /// </summary>
        /// <param name="file">Archivo de imagen recibido del cliente</param>
        /// <returns>Result con la URL pública del archivo guardado</returns>
        Task<Result<string>> SavePosterAsync(IFormFile file);
        Task<Result<int>> CreateAsync(MovieCreateDto dto);
        Task<Result<PagedResult<Movie>>> GetAllAsync(MovieQueryDto query);
        Task<Result<Movie?>> GetByIdAsync(int id);
        Task<Result<bool>> UpdateAsync(MovieUpdateDto dto);
        Task<Result<bool>> DeleteAsync(int id);
        // Otros métodos CRUD se agregarán luego
    }
} 