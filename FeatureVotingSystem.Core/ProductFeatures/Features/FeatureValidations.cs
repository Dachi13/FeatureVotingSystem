using FeatureVotingSystem.Core.ProductFeatures.Features.AddVote;
using FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureProperties;
using FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureStatus;
using FeatureVotingSystem.Core.ProductFeatures.Features.CreateFeature;
using FluentValidation.Results;

namespace FeatureVotingSystem.Core.ProductFeatures.Features;

public static class FeatureValidations
{
    public static ValidationResult ValidateCreateFeatureRequest(CreateFeatureRequest feature)
    {
        var validator = new CreateFeatureRequestValidator();
        var result = validator.Validate(feature);

        return result;
    }

    public static ValidationResult ValidateChangeFeaturePropertiesRequest(
        ChangeFeaturePropertiesRequest featureProperties)
    {
        var validator = new ChangeFeatureRequestValidator();
        var result = validator.Validate(featureProperties);

        return result;
    }

    public static ValidationResult ValidateChangeFeatureStatusRequest(ChangeFeatureStatusRequest featureStatusRequest)
    {
        var validator = new ChangeFeatureStatusRequestValidator();
        var result = validator.Validate(featureStatusRequest);

        return result;
    }

    public static ValidationResult ValidateVoteRequest(VoteRequest request)
    {
        var validator = new VoteRequestValidator();
        var result = validator.Validate(request);

        return result;
    }
}