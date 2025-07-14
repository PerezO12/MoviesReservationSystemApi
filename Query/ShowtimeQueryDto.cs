namespace Movie_Reservation_System.Query
{
    public class ShowtimeQueryDto
    {
        public int? MovieId { get; set; }
        public int? RoomId { get; set; }
        public DateTime? Date { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? OrderBy { get; set; } // Ej: "startTime"
        public bool Desc { get; set; } = false;
    }
} 