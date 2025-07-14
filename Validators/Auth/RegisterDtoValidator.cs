using FluentValidation;
using Movie_Reservation_System.DTOs.Auth;

namespace Movie_Reservation_System.Validators.Auth
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage("El email no es válido.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("El nombre completo es obligatorio.");
        }
    }
} 