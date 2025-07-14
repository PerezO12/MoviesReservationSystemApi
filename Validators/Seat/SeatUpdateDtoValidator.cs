using FluentValidation;
using Movie_Reservation_System.DTOs.Seat;

namespace Movie_Reservation_System.Validators.Seat
{
    public class SeatUpdateDtoValidator : AbstractValidator<SeatUpdateDto>
    {
        public SeatUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El Id es obligatorio y debe ser mayor que cero.");
            RuleFor(x => x.Row)
                .NotEmpty().WithMessage("La fila es obligatoria.")
                .MaximumLength(10).WithMessage("La fila no puede superar los 10 caracteres.");
            RuleFor(x => x.Number)
                .GreaterThan(0).WithMessage("El número de asiento debe ser mayor que cero.");
            RuleFor(x => x.RoomId)
                .GreaterThan(0).WithMessage("El RoomId es obligatorio y debe ser mayor que cero.");
        }
    }
} 