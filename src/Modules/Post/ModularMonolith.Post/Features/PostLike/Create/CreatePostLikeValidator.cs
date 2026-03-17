using FluentValidation;

namespace ModularMonolith.Post.Features.PostLike.Create;

public class CreatePostLikeValidator : AbstractValidator<CreatePostLikeCommand>
{
    public CreatePostLikeValidator()
    {
        RuleFor(x => x.PostId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
    }
}
