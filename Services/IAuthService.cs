using Movie_Reservation_System.DTOs.Auth;
using Movie_Reservation_System.Helpers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Movie_Reservation_System.Services
{
    public interface IAuthService
    {
        Task<Result<string>> RegisterAsync(RegisterDto dto, string? currentUserId, bool isCurrentUserAdmin);
        Task<Result<string>> LoginAsync(LoginDto dto);
        Task<Result<object>> GetCurrentUserAsync(string userId);
        Task<Result<bool>> ChangeUserRoleAsync(string userId, string newRole);
        Task<Result<string>> ForgotPasswordAsync(ForgotPasswordDto dto);
        Task<Result<bool>> ResetPasswordAsync(ResetPasswordDto dto);
        Task<Result<bool>> UpdateUserAsync(string userId, UpdateUserDto dto);
        Task<Result<List<object>>> GetAllUsersAsync();
    }
} 