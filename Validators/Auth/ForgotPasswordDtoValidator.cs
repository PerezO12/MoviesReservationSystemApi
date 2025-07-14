using FluentValidation;
using Movie_Reservation_System.DTOs.Auth;

namespace Movie_Reservation_System.Validators.Auth
{
    public class ForgotPasswordDtoValidator : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage("El email no es válido.");
        }
    }
} 