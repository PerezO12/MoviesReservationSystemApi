    using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Reservation_System.Models
{
    public class Seat
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Row { get; set; } = null!;

        [Required]
        public int Number { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room? Room { get; set; }

        // Navegación: reservas de asientos
        public ICollection<ReservationSeat>? ReservationSeats { get; set; }
    }
} 