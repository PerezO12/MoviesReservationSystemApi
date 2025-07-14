using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Reservation_System.Models
{
    public class ReservationSeat
    {
        public int ReservationId { get; set; }
        public Reservation? Reservation { get; set; }

        public int SeatId { get; set; }
        public Seat? Seat { get; set; }
    }
} 