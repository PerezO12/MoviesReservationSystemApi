using System.Collections.Generic;

namespace Movie_Reservation_System.DTOs.Reservation
{
    public class ReservationUpdateDto
    {
        public int Id { get; set; }
        public List<int> SeatIds { get; set; } = new();
    }
} 