using Microsoft.AspNetCore.Http;

namespace Movie_Reservation_System.DTOs.Movie
{
    public class UploadPosterDto
    {
        public IFormFile File { get; set; }
    }
} 