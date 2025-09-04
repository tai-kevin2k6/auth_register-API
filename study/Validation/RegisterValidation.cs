namespace study.Validation;
using study.Model.DTOs;
using FluentValidation;
public class RegisterValidation : AbstractValidator<RegisterRequest>
{
    public RegisterValidation()
    {
        RuleFor(user => user.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(8).WithMessage("Username must be at least 8 characters.");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[!@#$%^&*(),.?\"":{}|<>]").WithMessage("Password must contain at least one special character.");
    }
}

