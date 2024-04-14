using FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureProperties;
using FluentValidation.TestHelper;

namespace FeatureVotingSystem.Core.Tests.ProductFeatures.Features.ChangeFeatureProperties;

[TestFixture]
public class ChangeFeatureRequestValidatorTests
{
    private ChangeFeatureRequestValidator _validator;

    [SetUp]
    public void Setup()
    {
        _validator = new ChangeFeatureRequestValidator();
    }

    [Test]
    public void ShouldFailWhenNameIsEmpty()
    {
        var request = new ChangeFeaturePropertiesRequest
        {
            Name = "",
            Description = "Description",
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(feature => feature.Name)
            .WithErrorMessage("Name is Empty");
    }

    [Test]
    public void ShouldFailWhenNameLengthIsTooShort()
    {
        var request = new ChangeFeaturePropertiesRequest
        {
            Name = "A",
            Description = "Description",
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(feature => feature.Name)
            .WithErrorMessage("Length (1) of Name is Invalid");
    }

    [Test]
    public void ShouldFailWhenNameLengthIsTooLong()
    {
        var count = 51;

        var request = new ChangeFeaturePropertiesRequest
        {
            Name = new string('A', count),
            Description = "Description",
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(feature => feature.Name)
            .WithErrorMessage($"Length ({count}) of Name is Invalid");
    }

    [Test]
    public void ShouldFailWhenNameContainsSpecialCharacters()
    {
        var request = new ChangeFeaturePropertiesRequest
        {
            Name = "Dachi!@#$%^&*()+=\\[\\]{};':\"\\\\|,.<>\\/?",
            Description = "Description",
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(feature => feature.Name)
            .WithErrorMessage("Name contains invalid characters");
    }

    [Test]
    public void ShouldPassWhenNameIsValid()
    {
        var request = new ChangeFeaturePropertiesRequest
        {
            Name = "ValidName",
            Description = "Description",
        };

        var result = _validator.TestValidate(request);

        result.ShouldNotHaveValidationErrorFor(feature => feature.Name);
    }

    [Test]
    public void ShouldFailWhenDescriptionIsEmpty()
    {
        var request = new ChangeFeaturePropertiesRequest
        {
            Name = "ValidName",
            Description = "",
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(feature => feature.Description)
            .WithErrorMessage("Description is Empty");
    }

    [Test]
    public void ShouldFailWhenDescriptionLengthIsInvalid()
    {
        var count = 513;

        var request = new ChangeFeaturePropertiesRequest
        {
            Name = "ValidName",
            Description = new string('A', count),
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(feature => feature.Description)
            .WithErrorMessage($"Length ({count}) of Description is Invalid");
    }

    [Test]
    public void ShouldPassWhenDescriptionIsValid()
    {
        var request = new ChangeFeaturePropertiesRequest
        {
            Name = "ValidName",
            Description = "Valid Description",
        };

        var result = _validator.TestValidate(request);

        result.ShouldNotHaveValidationErrorFor(feature => feature.Description);
    }
}