using Microsoft.AspNetCore.Mvc;
using Movie_Reservation_System.Services;
using Movie_Reservation_System.DTOs.Seat;
using Movie_Reservation_System.Query;

namespace Movie_Reservation_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeatsController : ControllerBase
    {
        private readonly ISeatService _seatService;
        public SeatsController(ISeatService seatService)
        {
            _seatService = seatService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] SeatQueryDto query)
        {
            var result = await _seatService.GetAllAsync(query);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _seatService.GetByIdAsync(id);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SeatCreateDto dto)
        {
            var result = await _seatService.CreateAsync(dto);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SeatUpdateDto dto)
        {
            var result = await _seatService.UpdateAsync(dto);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _seatService.DeleteAsync(id);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }
    }
} 