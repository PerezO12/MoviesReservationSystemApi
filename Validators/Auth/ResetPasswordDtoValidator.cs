using FluentValidation;
using Movie_Reservation_System.DTOs.Auth;

namespace Movie_Reservation_System.Validators.Auth
{
    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage("El email no es válido.");
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("El token es obligatorio.");
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("La nueva contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");
        }
    }
} 