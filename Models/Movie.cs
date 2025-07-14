using System.ComponentModel.DataAnnotations;

namespace Movie_Reservation_System.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        /// <summary>
        /// URL pública del póster de la película (relativa o absoluta)
        /// nota: Esto solo es para fines de aprendizaje, en produccion
        ///      se debe usar un servicio de almacenamiento de archivos
        /// </summary>
        [MaxLength(500)]
        public string? PosterUrl { get; set; }

        [MaxLength(100)]
        public string? Genre { get; set; }

        // Navegación: lista de horarios
        public ICollection<Showtime>? Showtimes { get; set; }
    }
} 