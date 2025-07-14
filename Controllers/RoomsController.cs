using Microsoft.AspNetCore.Mvc;
using Movie_Reservation_System.Services;
using Movie_Reservation_System.DTOs.Room;
using Movie_Reservation_System.Query;

namespace Movie_Reservation_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] RoomQueryDto query)
        {
            var result = await _roomService.GetAllAsync(query);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _roomService.GetByIdAsync(id);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomCreateDto dto)
        {
            var result = await _roomService.CreateAsync(dto);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RoomUpdateDto dto)
        {
            var result = await _roomService.UpdateAsync(dto);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _roomService.DeleteAsync(id);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }
    }
} 