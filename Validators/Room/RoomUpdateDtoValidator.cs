using FluentValidation;
using Movie_Reservation_System.DTOs.Room;

namespace Movie_Reservation_System.Validators.Room
{
    public class RoomUpdateDtoValidator : AbstractValidator<RoomUpdateDto>
    {
        public RoomUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El Id es obligatorio y debe ser mayor que cero.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre de la sala es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");
        }
    }
} 