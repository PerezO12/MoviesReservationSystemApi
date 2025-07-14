using Microsoft.AspNetCore.Identity;

namespace Movie_Reservation_System.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string? FullName { get; set; }

    }
} 