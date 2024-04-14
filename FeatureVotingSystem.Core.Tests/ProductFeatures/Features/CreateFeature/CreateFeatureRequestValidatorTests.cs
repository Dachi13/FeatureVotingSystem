using FeatureVotingSystem.Core.ProductFeatures.Features.CreateFeature;
using FluentValidation.TestHelper;

namespace FeatureVotingSystem.Core.Tests.ProductFeatures.Features.CreateFeature;

[TestFixture]
public class CreateFeatureRequestValidatorTests
{
    private CreateFeatureRequestValidator _validator;

    [SetUp]
    public void Setup()
    {
        _validator = new CreateFeatureRequestValidator();
    }

    [Test]
    public void ShouldPassWhenNameIsValid()
    {
        var request = new CreateFeatureRequest
        {
            Name = "Valid Name",
            Description = "Description",
            ProductId = 7
        };

        var result = _validator.TestValidate(request);

        result.ShouldNotHaveValidationErrorFor(feature => feature.Name);
    }

    [Test]
    public void ShouldFailWhenNameIsEmpty()
    {
        var request = new CreateFeatureRequest
        {
            Name = "",
            Description = "Description",
            ProductId = 7
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(feature => feature.Name)
            .WithErrorMessage("Name is Empty");
    }

    [Test]
    public void ShouldFailWhenNameLengthIsTooShort()
    {
        var request = new CreateFeatureRequest
        {
            Name = "A",
            Description = "Description",
            ProductId = 7
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(feature => feature.Name)
            .WithErrorMessage("Length (1) of Name is Invalid");
    }

    [Test]
    public void ShouldFailWhenNameLengthIsTooLong()
    {
        var count = 51;

        var request = new CreateFeatureRequest
        {
            Name = new string('A', count),
            Description = "Description",
            ProductId = 7
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(feature => feature.Name)
            .WithErrorMessage($"Length ({count}) of Name is Invalid");
    }

    [Test]
    public void ShouldFailWhenNameContainsSpecialCharacters()
    {
        var request = new CreateFeatureRequest
        {
            Name = "Dachi!@#$%^&*()+=\\[\\]{};':\"\\\\|,.<>\\/?]",
            Description = "Description",
            ProductId = 7
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(feature => feature.Name)
            .WithErrorMessage("Name contains invalid characters");
    }

    [Test]
    public void ShouldFailWhenDescriptionIsEmpty()
    {
        var request = new CreateFeatureRequest
        {
            Name = "ValidName",
            Description = "",
            ProductId = 7
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(feature => feature.Description)
            .WithErrorMessage("Description is Empty");
    }

    [Test]
    public void ShouldFailWhenDescriptionLengthIsInvalid()
    {
        var count = 513;

        var request = new CreateFeatureRequest
        {
            Name = "ValidName",
            Description = new string('A', count),
            ProductId = 7
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(feature => feature.Description)
            .WithErrorMessage($"Length ({count}) of Description is Invalid");
    }

    [Test]
    public void ShouldPassWhenDescriptionIsValid()
    {
        var request = new CreateFeatureRequest
        {
            Name = "ValidName",
            Description = "Valid Description",
            ProductId = 7
        };

        var result = _validator.TestValidate(request);

        result.ShouldNotHaveValidationErrorFor(feature => feature.Description);
    }
}