using FeatureVotingSystem.Shared.Entities.Exceptions;

namespace FeatureVotingSystem.Core.Comments.Features.AddComment;

public class AddCommentRequest
{
    public int UserId { get; private set; }
    public int FeatureId { get; set; }
    public DateTime CreatedAt { get; } = DateTime.Now;
    public string Text { get; set; }

    public void SetUserId(int userId)
    {
        UserId = userId;
        Validate();
    }

    private void Validate()
    {
        var validationResult = CommentValidations.ValidateCommentRequest(this);

        if (!validationResult.IsValid)
            throw new CommentBadRequestException(validationResult.Errors.First().ErrorMessage);
    }
}