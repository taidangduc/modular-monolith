using FluentValidation;

namespace Identity.Features.Create;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Password).NotEmpty().WithMessage("Please enter the password");
        RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Please enter the confirm password");

        RuleFor(x => x).Custom((x, context) =>
        {
            if (x.Password != x.ConfirmPassword)
            {
                context.AddFailure(nameof(x.Password), "Password not match");
            }
        });

        RuleFor(x => x.UserName).NotEmpty().WithMessage("Please enter the username");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Please enter the firstname");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Please enter the lastname");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Please enter the email")
            .EmailAddress().WithMessage("A valid email is required");
    }
}
