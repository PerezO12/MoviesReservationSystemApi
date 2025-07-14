using FluentValidation;
using Movie_Reservation_System.DTOs.Reservation;

namespace Movie_Reservation_System.Validators.Reservation
{
    public class ReservationCreateDtoValidator : AbstractValidator<ReservationCreateDto>
    {
        public ReservationCreateDtoValidator()
        {
            RuleFor(x => x.ShowtimeId)
                .GreaterThan(0).WithMessage("El ShowtimeId es obligatorio y debe ser mayor que cero.");
            RuleFor(x => x.SeatIds)
                .NotEmpty().WithMessage("Debe seleccionar al menos un asiento.");
        }
    }
} 