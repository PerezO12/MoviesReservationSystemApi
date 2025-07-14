using Microsoft.AspNetCore.Mvc;
using Movie_Reservation_System.Services;
using Movie_Reservation_System.DTOs.Reservation;
using Movie_Reservation_System.Query;
using System.Security.Claims;

namespace Movie_Reservation_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ReservationQueryDto query)
        {
            var result = await _reservationService.GetAllAsync(query);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _reservationService.GetByIdAsync(id);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReservationCreateDto dto)
        {
            // Simulación: obtener el userId del usuario autenticado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "test-user";
            var result = await _reservationService.CreateAsync(dto, userId);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ReservationUpdateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "test-user";
            var result = await _reservationService.UpdateAsync(dto, userId);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "test-user";
            var result = await _reservationService.DeleteAsync(id, userId);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }
    }
} 