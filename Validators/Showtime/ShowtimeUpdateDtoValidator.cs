using FluentValidation;
using Movie_Reservation_System.DTOs.Showtime;

namespace Movie_Reservation_System.Validators.Showtime
{
    public class ShowtimeUpdateDtoValidator : AbstractValidator<ShowtimeUpdateDto>
    {
        public ShowtimeUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El Id es obligatorio y debe ser mayor que cero.");
            RuleFor(x => x.MovieId)
                .GreaterThan(0).WithMessage("El MovieId es obligatorio y debe ser mayor que cero.");
            RuleFor(x => x.RoomId)
                .GreaterThan(0).WithMessage("El RoomId es obligatorio y debe ser mayor que cero.");
            RuleFor(x => x.StartTime)
                .GreaterThan(DateTime.MinValue).WithMessage("La fecha y hora de inicio es obligatoria.");
        }
    }
} 