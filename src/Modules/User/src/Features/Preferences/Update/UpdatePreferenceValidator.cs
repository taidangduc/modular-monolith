using FluentValidation;

namespace User.Features.Preferences.Update;

public class UpdatePreferenceValidator : AbstractValidator<UpdatePreferenceCommand>
{
    public UpdatePreferenceValidator()
    {
        RuleFor(x => x.Channel).IsInEnum().WithMessage("Channel must be Email, Sms or Web");
    }
}
