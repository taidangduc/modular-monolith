using FluentValidation;

namespace ModularMonolith.Preference.Features.Update;

public class UpdatePreferenceValidator : AbstractValidator<UpdatePreferenceCommand>
{
    public UpdatePreferenceValidator()
    {
        RuleFor(x => x.Channel).IsInEnum().WithMessage("Channel must be Email, Sms or Web");
    }
}
