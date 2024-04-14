using FeatureVotingSystem.Core.Products.Features.CreateProduct;
using FluentValidation.TestHelper;

namespace FeatureVotingSystem.Core.Tests.Products.Features.CreateProduct;

[TestFixture]
public class CreateProductRequestValidatorTests
{
    private readonly CreateProductRequestValidator _validator;
    const string EMPTY_NAME_ERR_MESSAGE = "Name is Empty";
    const string INVALID_NAME_ERR_MESSAGE = "Name contains invalid characters";
    const string EMPTY_DESC_ERR_MESSAGE = "Short Desc is Empty";

    public CreateProductRequestValidatorTests()
    {
        _validator = new CreateProductRequestValidator();
    }

    private CreateProductRequest CreateRequest(string name, string shortDesc) =>
        new CreateProductRequest() { Name = name, ShortDesc = shortDesc };

    [TestCase("", "Test description", EMPTY_NAME_ERR_MESSAGE)]
    [TestCase("Gaga!", "Test description", INVALID_NAME_ERR_MESSAGE)]
    [TestCase("Gag#a", "Test description", INVALID_NAME_ERR_MESSAGE)]
    [TestCase("()Gaga", "Test description", INVALID_NAME_ERR_MESSAGE)]
    [TestCase("[Gaga]", "Test description", INVALID_NAME_ERR_MESSAGE)]
    [TestCase("Gaga/Deme", "Test description", INVALID_NAME_ERR_MESSAGE)]
    public void ShouldFailOnEmtyOrInvalidName(string name, string shortDesc, string errorMessage)
    {
        var request = CreateRequest(name, shortDesc);
        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(product => product.Name)
            .WithErrorMessage(errorMessage);
    }

    [TestCase("Gaga", "", EMPTY_DESC_ERR_MESSAGE)]
    public void ShouldFailOnEmptyDescription(string name, string shortDesc, string errorMessage)
    {
        var request = CreateRequest(name, shortDesc);
        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(product => product.ShortDesc)
            .WithErrorMessage(errorMessage);
    }

    [TestCase(1, "Test")]
    [TestCase(31, "Test")]
    public void ShouldFailWhenNameLengthIsInvalid(int count, string shortDesc)
    {
        var request = CreateRequest(new string('A', count), shortDesc);
        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(product => product.Name)
            .WithErrorMessage($"Length ({count}) of Name is Invalid");
    }

    [TestCase("Gaga", "Test")]
    public void ShouldPassWhenNameIsValid(string name, string shortDesc)
    {
        var request = CreateRequest(name, shortDesc);
        var result = _validator.TestValidate(request);

        result.ShouldNotHaveValidationErrorFor(product => product.Name);
    }

    [TestCase(513, "Gaga")]
    public void ShouldFailWhenDescriptionLengthIsInvalid(int count, string name)
    {
        var request = CreateRequest(name, new string('A', count));
        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(product => product.ShortDesc)
            .WithErrorMessage($"Length ({count}) of Short Desc is Invalid");
    }

    [TestCase("Gaga", "Short description")]
    public void ShouldPassWhenDescriptionIsValid(string name, string shortDesc)
    {
        var request = CreateRequest(name, shortDesc);
        var result = _validator.TestValidate(request);

        result.ShouldNotHaveValidationErrorFor(product => product.ShortDesc);
    }
}
