namespace Movie_Reservation_System.DTOs.Showtime
{
    public class ShowtimeUpdateDto
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartTime { get; set; }
    }
} 