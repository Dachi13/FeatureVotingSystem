using FeatureVotingSystem.Shared.Entities.Exceptions;

namespace FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureStatus;

public class ChangeFeatureStatusRequest
{
    public int UserId { get; private set; }
    public int FeatureId { get; set; }
    public int StatusId { get; set; }
    public string RejectionReason { get; set; }

    public void SetUserId(int userId)
    {
        UserId = userId;
        Validate();
    }

    private void Validate()
    {
        var validationResult = FeatureValidations.ValidateChangeFeatureStatusRequest(this);

        if (!validationResult.IsValid)
            throw new FeatureBadRequestException(validationResult.Errors.First().ErrorMessage);
    }
}