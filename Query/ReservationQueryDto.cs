namespace Movie_Reservation_System.Query
{
    public class ReservationQueryDto
    {
        public int? UserId { get; set; }
        public int? ShowtimeId { get; set; }
        public string? Status { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? OrderBy { get; set; } // Ej: "createdAt"
        public bool Desc { get; set; } = false;
    }
} 