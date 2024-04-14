using FluentValidation;

namespace FeatureVotingSystem.Core.Comments.Features.AddComment;

public sealed class AddCommentValidator : AbstractValidator<AddCommentRequest>
{
    public AddCommentValidator()
    {
        RuleFor(comment => comment.Text)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Length(1, 512)
            .WithMessage("{PropertyName} must not exceed 512 characters");
    }
}