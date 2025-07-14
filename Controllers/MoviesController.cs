using Microsoft.AspNetCore.Mvc;
using Movie_Reservation_System.Services;
using Movie_Reservation_System.Query;

namespace Movie_Reservation_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Sube un archivo de póster y retorna la URL pública.
        /// </summary>
        /// <param name="file">Archivo de imagen (form-data)</param>
        /// <returns>URL pública del póster</returns>
        [HttpPost("upload-poster")]
        public async Task<IActionResult> UploadPoster([FromForm] IFormFile file)
        {
            var result = await _movieService.SavePosterAsync(file);
            if (result.Success)
                return StatusCode((int)result.StatusCode, new { url = result.Data, message = result.Message });
            else
                return StatusCode((int)result.StatusCode, new { error = result.Message, errors = result.Errors });
        }

        /// <summary>
        /// Obtiene una lista paginada y filtrada de películas.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] MovieQueryDto query)
        {
            var result = await _movieService.GetAllAsync(query);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Obtiene una película por Id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _movieService.GetByIdAsync(id);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Crea una nueva película.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DTOs.Movie.MovieCreateDto dto)
        {
            var result = await _movieService.CreateAsync(dto);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Actualiza una película existente.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DTOs.Movie.MovieUpdateDto dto)
        {
            var result = await _movieService.UpdateAsync(dto);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Elimina una película por Id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _movieService.DeleteAsync(id);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }
    }
} 