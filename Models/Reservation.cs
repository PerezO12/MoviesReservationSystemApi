using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Reservation_System.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;
        public ApplicationUser? User { get; set; }

        [ForeignKey("Showtime")]
        public int ShowtimeId { get; set; }
        public Showtime? Showtime { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Active"; // Active, Cancelled, etc.

        // Navegación: asientos reservados
        public ICollection<ReservationSeat>? ReservationSeats { get; set; }
    }
} 