namespace Movie_Reservation_System.DTOs.Auth
{
    public class UpdateUserDto
    {
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
    }
} 