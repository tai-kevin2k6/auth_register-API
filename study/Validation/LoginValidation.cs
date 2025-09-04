namespace study.Validation;
using study.Model.DTOs;
using FluentValidation;
public class LoginValidation : AbstractValidator<LoginRequest>
{
    public LoginValidation()
    {
        RuleFor(user => user.Username)
            .NotEmpty().WithMessage("Username is required.");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}

