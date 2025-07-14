namespace Movie_Reservation_System.DTOs.Movie
{
    public class MovieCreateDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Genre { get; set; }
        public string? PosterUrl { get; set; }
    }
} 