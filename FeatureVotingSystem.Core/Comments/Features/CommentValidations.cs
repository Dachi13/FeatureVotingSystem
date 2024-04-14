using FeatureVotingSystem.Core.Comments.Features.AddComment;
using FluentValidation.Results;

namespace FeatureVotingSystem.Core.Comments.Features;

public static class CommentValidations
{
    public static ValidationResult ValidateCommentRequest(AddCommentRequest request)
    {
        var validator = new AddCommentValidator();
        var result = validator.Validate(request);

        return result;
    }
}