namespace Movie_Reservation_System.Query
{
    public class SeatQueryDto
    {
        public string? Row { get; set; }
        public int? Number { get; set; }
        public int? RoomId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? OrderBy { get; set; } // Ej: "row", "number"
        public bool Desc { get; set; } = false;
    }
} 