namespace Movie_Reservation_System.Query
{
    public class MovieQueryDto
    {
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? OrderBy { get; set; } // Ej: "title", "genre"
        public bool Desc { get; set; } = false;
    }
} 