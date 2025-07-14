using System.ComponentModel.DataAnnotations;

namespace Movie_Reservation_System.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        // Navegación: asientos y horarios
        public ICollection<Seat>? Seats { get; set; }
        public ICollection<Showtime>? Showtimes { get; set; }
    }
} 