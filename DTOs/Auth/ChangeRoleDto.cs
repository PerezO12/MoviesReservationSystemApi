namespace Movie_Reservation_System.DTOs.Auth
{
    public class ChangeRoleDto
    {
        public string UserId { get; set; } = null!;
        public string NewRole { get; set; } = null!;
    }
} 