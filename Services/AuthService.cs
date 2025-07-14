using Movie_Reservation_System.DTOs.Auth;
using Movie_Reservation_System.Helpers;
using Movie_Reservation_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Movie_Reservation_System.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }

        public async Task<Result<string>> RegisterAsync(RegisterDto dto, string? currentUserId, bool isCurrentUserAdmin)
        {
            // Solo un admin puede crear usuarios admin
            if (!string.IsNullOrEmpty(dto.Role) && dto.Role.ToLower() == "admin" && !isCurrentUserAdmin)
                return Result<string>.Fail("Solo un administrador puede crear usuarios admin.", System.Net.HttpStatusCode.Forbidden);

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return Result<string>.Fail(result.Errors.Select(e => e.Description).ToList());

            var role = string.IsNullOrEmpty(dto.Role) ? "User" : dto.Role;
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));
            await _userManager.AddToRoleAsync(user, role);

            return Result<string>.Ok(user.Id, "Usuario registrado correctamente.");
        }

        public async Task<Result<string>> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Result<string>.Fail("Credenciales inválidas.", System.Net.HttpStatusCode.Unauthorized);
            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                return Result<string>.Fail("Credenciales inválidas.", System.Net.HttpStatusCode.Unauthorized);

            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateJwtToken(user, roles);
            return Result<string>.Ok(token, "Login exitoso.");
        }

        public async Task<Result<object>> GetCurrentUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Result<object>.Fail("Usuario no encontrado.", System.Net.HttpStatusCode.NotFound);
            var roles = await _userManager.GetRolesAsync(user);
            return Result<object>.Ok(new { user.Id, user.Email, user.FullName, Roles = roles });
        }

        public async Task<Result<bool>> ChangeUserRoleAsync(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Result<bool>.Fail("Usuario no encontrado.", System.Net.HttpStatusCode.NotFound);
            if (!await _roleManager.RoleExistsAsync(newRole))
                await _roleManager.CreateAsync(new IdentityRole(newRole));
            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
                return Result<bool>.Fail(removeResult.Errors.Select(e => e.Description).ToList());
            var addResult = await _userManager.AddToRoleAsync(user, newRole);
            if (!addResult.Succeeded)
                return Result<bool>.Fail(addResult.Errors.Select(e => e.Description).ToList());
            return Result<bool>.Ok(true, $"Rol cambiado a {newRole} correctamente.");
        }

        public async Task<Result<string>> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Result<string>.Fail("Usuario no encontrado.", System.Net.HttpStatusCode.NotFound);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // En producción, aquí se enviaría el token por email
            return Result<string>.Ok(token, "Token de reseteo generado. (En producción se enviaría por email)");
        }

        public async Task<Result<bool>> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Result<bool>.Fail("Usuario no encontrado.", System.Net.HttpStatusCode.NotFound);
            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
            if (!result.Succeeded)
                return Result<bool>.Fail(result.Errors.Select(e => e.Description).ToList());
            return Result<bool>.Ok(true, "Contraseña reseteada correctamente.");
        }

        public async Task<Result<bool>> UpdateUserAsync(string userId, UpdateUserDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Result<bool>.Fail("Usuario no encontrado.", System.Net.HttpStatusCode.NotFound);

            if (!string.IsNullOrEmpty(dto.Email) && dto.Email != user.Email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, dto.Email);
                if (!setEmailResult.Succeeded)
                    return Result<bool>.Fail(setEmailResult.Errors.Select(e => e.Description).ToList());
                user.UserName = dto.Email;
            }
            if (!string.IsNullOrEmpty(dto.FullName))
                user.FullName = dto.FullName;

            if (!string.IsNullOrEmpty(dto.NewPassword))
            {
                if (string.IsNullOrEmpty(dto.CurrentPassword))
                    return Result<bool>.Fail("Debes ingresar la contraseña actual para cambiar la contraseña.");
                var changePassResult = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
                if (!changePassResult.Succeeded)
                    return Result<bool>.Fail(changePassResult.Errors.Select(e => e.Description).ToList());
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return Result<bool>.Fail(updateResult.Errors.Select(e => e.Description).ToList());
            return Result<bool>.Ok(true, "Usuario actualizado correctamente.");
        }

        public async Task<Result<List<object>>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();
            var result = new List<object>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new { user.Id, user.Email, user.FullName, Roles = roles });
            }
            return Result<List<object>>.Ok(result);
        }

        private string GenerateJwtToken(ApplicationUser user, System.Collections.Generic.IList<string> roles)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim("fullName", user.FullName ?? "")
            }.ToList();
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? "supersecretkey"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddHours(8);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
} 