using FluentValidation;
using Movie_Reservation_System.DTOs.Movie;

namespace Movie_Reservation_System.Validators.Movie
{
    public class MovieUpdateDtoValidator : AbstractValidator<MovieUpdateDto>
    {
        public MovieUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El Id es obligatorio y debe ser mayor que cero.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("El título es obligatorio.")
                .MaximumLength(200).WithMessage("El título no puede superar los 200 caracteres.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("La descripción no puede superar los 1000 caracteres.");

            RuleFor(x => x.Genre)
                .MaximumLength(100).WithMessage("El género no puede superar los 100 caracteres.");

            RuleFor(x => x.PosterUrl)
                .MaximumLength(500).WithMessage("La URL del póster no puede superar los 500 caracteres.");
        }
    }
} 