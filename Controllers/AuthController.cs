using Microsoft.AspNetCore.Mvc;
using Movie_Reservation_System.Services;
using Movie_Reservation_System.DTOs.Auth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Movie_Reservation_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");
            var result = await _authService.RegisterAsync(dto, userId, isAdmin);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { error = "No autenticado" });
            var result = await _authService.GetCurrentUserAsync(userId);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { error = "No autenticado" });
            var result = await _authService.UpdateUserAsync(userId, dto);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("change-role")]
        public async Task<IActionResult> ChangeRole([FromBody] ChangeRoleDto dto)
        {
            if (!User.IsInRole("Admin"))
                return Forbid();
            var result = await _authService.ChangeUserRoleAsync(dto.UserId, dto.NewRole);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            var result = await _authService.ForgotPasswordAsync(dto);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var result = await _authService.ResetPasswordAsync(dto);
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            if (!User.IsInRole("Admin"))
                return Forbid();
            var result = await _authService.GetAllUsersAsync();
            if (result.Success)
                return Ok(result);
            else
                return StatusCode((int)result.StatusCode, result);
        }
    }
} 