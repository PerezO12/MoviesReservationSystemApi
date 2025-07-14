namespace Movie_Reservation_System.DTOs.Showtime
{
    public class ShowtimeCreateDto
    {
        public int MovieId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartTime { get; set; }
    }
} 