using FeatureVotingSystem.Shared.Entities.Exceptions;

namespace FeatureVotingSystem.Core.ProductFeatures.Features.CreateFeature;

public class CreateFeatureRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int UserId { get; private set; }
    public int ProductId { get; set; }
    public DateTime UploadDate { get; } = DateTime.Now;

    public void SetUserId(int userId)
    {
        UserId = userId;
        Validate();
    }

    private void Validate()
    {
        var validationResult = FeatureValidations.ValidateCreateFeatureRequest(this);

        if (!validationResult.IsValid)
            throw new FeatureBadRequestException(validationResult.Errors.First().ErrorMessage);
    }
}