using FluentValidation;
using Movie_Reservation_System.DTOs.Auth;

namespace Movie_Reservation_System.Validators.Auth
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("El email no es válido.");
            RuleFor(x => x.NewPassword)
                .MinimumLength(6).When(x => !string.IsNullOrEmpty(x.NewPassword)).WithMessage("La nueva contraseña debe tener al menos 6 caracteres.");
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().When(x => !string.IsNullOrEmpty(x.NewPassword)).WithMessage("Debes ingresar la contraseña actual para cambiar la contraseña.");
        }
    }
} 