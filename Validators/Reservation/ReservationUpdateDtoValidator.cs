using FluentValidation;
using Movie_Reservation_System.DTOs.Reservation;

namespace Movie_Reservation_System.Validators.Reservation
{
    public class ReservationUpdateDtoValidator : AbstractValidator<ReservationUpdateDto>
    {
        public ReservationUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El Id es obligatorio y debe ser mayor que cero.");
            RuleFor(x => x.SeatIds)
                .NotEmpty().WithMessage("Debe seleccionar al menos un asiento.");
        }
    }
} 