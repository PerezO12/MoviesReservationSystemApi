using FluentValidation;
using Movie_Reservation_System.DTOs.Room;

namespace Movie_Reservation_System.Validators.Room
{
    public class RoomCreateDtoValidator : AbstractValidator<RoomCreateDto>
    {
        public RoomCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre de la sala es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");
        }
    }
} 