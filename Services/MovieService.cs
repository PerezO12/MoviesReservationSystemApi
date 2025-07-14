using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using Movie_Reservation_System.Helpers;
using System.Net;
using Movie_Reservation_System.DTOs.Movie;
using Movie_Reservation_System.Models;
using Movie_Reservation_System.Repositories;
using FluentValidation;
using System.Linq;
using Movie_Reservation_System.Query;

namespace Movie_Reservation_System.Services
{
    public class MovieService : IMovieService
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _posterFolder = "images/posters";
        private readonly string[] _allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        private const long MaxFileSize = 2 * 1024 * 1024; // 2MB
        private readonly IMovieRepository _repo;
        private readonly IValidator<MovieCreateDto> _validator;
        private readonly Data.ApplicationDbContext _db;

        public MovieService(IWebHostEnvironment env, IMovieRepository repo, IValidator<MovieCreateDto> validator, Data.ApplicationDbContext db)
        {
            _env = env;
            _repo = repo;
            _validator = validator;
            _db = db;
        }

        public async Task<Result<int>> CreateAsync(MovieCreateDto dto)
        {
            var validation = await _validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Result<int>.Fail(validation.Errors.Select(e => e.ErrorMessage).ToList());

            var movie = new Movie
            {
                Title = dto.Title,
                Description = dto.Description,
                Genre = dto.Genre,
                PosterUrl = dto.PosterUrl
            };

            await _repo.AddAsync(movie);
            await _db.SaveChangesAsync();

            return Result<int>.Ok(movie.Id, "Película creada correctamente.");
        }

        public async Task<Result<string>> SavePosterAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Result<string>.Fail("El archivo es inválido.", HttpStatusCode.BadRequest);

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (Array.IndexOf(_allowedExtensions, ext) < 0)
                return Result<string>.Fail("Tipo de archivo no permitido. Solo jpg, jpeg, png, webp.", HttpStatusCode.UnsupportedMediaType);

            if (file.Length > MaxFileSize)
                return Result<string>.Fail("El archivo excede el tamaño máximo permitido (2MB).", HttpStatusCode.RequestEntityTooLarge);

            var fileName = $"poster_{Guid.NewGuid()}{ext}";
            var savePath = Path.Combine(_env.WebRootPath, _posterFolder, fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);

            try
            {
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                return Result<string>.Fail($"Error al guardar el archivo: {ex.Message}", HttpStatusCode.InternalServerError);
            }

            var url = $"/{_posterFolder.Replace("\\", "/")}/{fileName}";
            return Result<string>.Ok(url, "Archivo subido correctamente.");
        }

        public async Task<Result<PagedResult<Movie>>> GetAllAsync(MovieQueryDto query)
        {
            try
            {
                var result = await _repo.GetAllAsync(query);
                return Result<PagedResult<Movie>>.Ok(result);
            }
            catch (Exception ex)
            {
                return Result<PagedResult<Movie>>.Fail($"Error al obtener películas: {ex.Message}", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result<Movie?>> GetByIdAsync(int id)
        {
            var movie = await _repo.GetByIdAsync(id);
            if (movie == null)
                return Result<Movie?>.Fail("Película no encontrada.", System.Net.HttpStatusCode.NotFound);
            return Result<Movie?>.Ok(movie);
        }

        public async Task<Result<bool>> UpdateAsync(MovieUpdateDto dto)
        {
            var validation = await new Validators.Movie.MovieUpdateDtoValidator().ValidateAsync(dto);
            if (!validation.IsValid)
                return Result<bool>.Fail(validation.Errors.Select(e => e.ErrorMessage).ToList());

            var movie = await _repo.GetByIdAsync(dto.Id);
            if (movie == null)
                return Result<bool>.Fail("Película no encontrada.", System.Net.HttpStatusCode.NotFound);

            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.Genre = dto.Genre;
            movie.PosterUrl = dto.PosterUrl;

            await _repo.UpdateAsync(movie);
            await _db.SaveChangesAsync();
            return Result<bool>.Ok(true, "Película actualizada correctamente.");
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var movie = await _repo.GetByIdAsync(id);
            if (movie == null)
                return Result<bool>.Fail("Película no encontrada.", System.Net.HttpStatusCode.NotFound);
            await _repo.DeleteAsync(movie);
            await _db.SaveChangesAsync();
            return Result<bool>.Ok(true, "Película eliminada correctamente.");
        }
    }
} 