using FeatureVotingSystem.Shared.Entities.Exceptions;

namespace FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureProperties;

public class ChangeFeaturePropertiesRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int UserId { get; private set; }

    public void SetUserId(int userId)
    {
        UserId = userId;
        Validate();
    }

    private void Validate()
    {
        var validationResult = FeatureValidations.ValidateChangeFeaturePropertiesRequest(this);

        if (!validationResult.IsValid)
            throw new FeatureBadRequestException(validationResult.Errors.First().ErrorMessage);
    }
}