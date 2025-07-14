namespace Movie_Reservation_System.DTOs.Seat
{
    public class SeatCreateDto
    {
        public string Row { get; set; } = null!;
        public int Number { get; set; }
        public int RoomId { get; set; }
    }
} 