namespace Movie_Reservation_System.Query
{
    public class RoomQueryDto
    {
        public string? Name { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? OrderBy { get; set; } // Ej: "name"
        public bool Desc { get; set; } = false;
    }
} 