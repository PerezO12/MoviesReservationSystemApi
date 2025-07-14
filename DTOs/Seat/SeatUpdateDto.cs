namespace Movie_Reservation_System.DTOs.Seat
{
    public class SeatUpdateDto
    {
        public int Id { get; set; }
        public string Row { get; set; } = null!;
        public int Number { get; set; }
        public int RoomId { get; set; }
    }
} 