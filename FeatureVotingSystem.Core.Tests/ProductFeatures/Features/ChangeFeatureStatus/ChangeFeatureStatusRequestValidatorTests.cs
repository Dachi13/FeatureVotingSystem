using FeatureVotingSystem.Core.ProductFeatures.Enums;
using FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureStatus;
using FluentValidation.TestHelper;

namespace FeatureVotingSystem.Core.Tests.ProductFeatures.Features.ChangeFeatureStatus;

[TestFixture]
public class ChangeFeatureStatusRequestValidatorTests
{
    private ChangeFeatureStatusRequestValidator _validator;

    [SetUp]
    public void Setup()
    {
        _validator = new ChangeFeatureStatusRequestValidator();
    }

    [Test]
    public void ShouldFailWhenStatusIsEmpty()
    {
        var request = new ChangeFeatureStatusRequest();

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(feature => feature);
    }

    [Test]
    public void ShouldFailWhenStatusIdIsInvalid()
    {
        var request = new ChangeFeatureStatusRequest
        {
            StatusId = 0
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(feature => feature);
    }

    [Test]
    public void ShouldPassWhenStatusIsValid()
    {
        var request = new ChangeFeatureStatusRequest
        {
            StatusId = (int)Status.InProgress
        };

        var result = _validator.TestValidate(request);

        result.ShouldNotHaveValidationErrorFor(feature => feature.StatusId);
    }

    [Test]
    public void ShouldFailWhenRejectionReasonIsMissing()
    {
        var request = new ChangeFeatureStatusRequest
        {
            StatusId = (int)Status.Rejected
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(feature => feature);
    }

    [Test]
    public void ShouldNotFailWhenRejectingFeature()
    {
        var request = new ChangeFeatureStatusRequest
        {
            StatusId = (int)Status.Rejected,
            RejectionReason = "Rejection"
        };

        var result = _validator.TestValidate(request);

        result.ShouldNotHaveValidationErrorFor(feature => feature);
    }

    [Test]
    public void ShouldFailWhenRejectionReasonIsTooLong()
    {
        var request = new ChangeFeatureStatusRequest
        {
            StatusId = (int)Status.Rejected,
            RejectionReason = new string('A', 1042)
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(feature => feature);
    }

    [Test]
    public void ShouldFailWhenRejectionReasonIsProvidedButStatusIdIsInvalid()
    {
        var request = new ChangeFeatureStatusRequest
        {
            StatusId = 12,
            RejectionReason = "Rejecting"
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(feature => feature);
    }
}