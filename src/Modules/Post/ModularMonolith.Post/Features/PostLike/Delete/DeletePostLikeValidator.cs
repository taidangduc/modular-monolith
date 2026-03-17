using FluentValidation;

namespace ModularMonolith.Post.Features.PostLike.Delete;

public class DeletePostLikeValidator : AbstractValidator<DeletePostLikeCommand>
{
    public DeletePostLikeValidator()
    {
        RuleFor(x => x.PostId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
    }
}
