using FluentValidation;

namespace ModularMonolith.Profile.Features.Update;

public class UpdateProfileValidator : AbstractValidator<UpdateProfileCommand>
{
    public UpdateProfileValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("Please enter UserId");
        RuleFor(x => x.GenderType).IsInEnum().WithMessage("Please enter a valid GenderType");
        RuleFor(x => x.Age).GreaterThan(0).WithMessage("Age is not a negative value");
    }
}
