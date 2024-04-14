using FeatureVotingSystem.Core.Products.Features.UpdateProduct;
using FluentValidation.TestHelper;

namespace FeatureVotingSystem.Core.Tests.Products.Features.UpdateProduct;

public class UpdateProductRequestValidatorTests
{
    private readonly UpdateProductRequestValidator _validator;
    const string EMPTY_NAME_ERR_MESSAGE = "Name is Empty";
    const string INVALID_NAME_ERR_MESSAGE = "Name contains invalid characters";
    const string EMPTY_DESC_ERR_MESSAGE = "Short Desc is Empty";

    public UpdateProductRequestValidatorTests()
    {
        _validator = new UpdateProductRequestValidator();
    }

    public UpdateProductRequest CreateRequest(int id, string name, string shortDesc) =>
        new UpdateProductRequest(id, name, shortDesc);

    [TestCase(0, "Gaga", "Short Description")]
    public void ShouldFailOnWrongId(int id, string name, string shortDesc)
    {
        var request = CreateRequest(id, name, shortDesc);
        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(product => product.Id)
            .WithErrorMessage($"Request contains invalid Id: {id}. Id has to be greater than 0.");
    }

    [TestCase(1, "", "Test description", EMPTY_NAME_ERR_MESSAGE)]
    [TestCase(1, "Gaga!", "Test description", INVALID_NAME_ERR_MESSAGE)]
    [TestCase(1, "Gag#a", "Test description", INVALID_NAME_ERR_MESSAGE)]
    [TestCase(1, "()Gaga", "Test description", INVALID_NAME_ERR_MESSAGE)]
    [TestCase(1, "[Gaga]", "Test description", INVALID_NAME_ERR_MESSAGE)]
    [TestCase(1, "Gaga/Deme", "Test description", INVALID_NAME_ERR_MESSAGE)]
    public void ShouldFailOnEmtyOrInvalidName(int id, string name, string shortDesc, string errorMessage)
    {
        var request = CreateRequest(id, name, shortDesc);
        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(product => product.Name)
            .WithErrorMessage(errorMessage);
    }

    [TestCase(1, "Gaga", "", EMPTY_DESC_ERR_MESSAGE)]
    public void ShouldFailOnEmptyDescription(int id, string name, string shortDesc, string errorMessage)
    {
        var request = CreateRequest(id, name, shortDesc);
        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(product => product.ShortDesc)
            .WithErrorMessage(errorMessage);
    }

    [TestCase(1, 1, "Test")]
    [TestCase(1, 31, "Test")]
    public void ShouldFailWhenNameLengthIsInvalid(int id, int count, string shortDesc)
    {
        var request = CreateRequest(id, new string('A', count), shortDesc);
        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(product => product.Name)
            .WithErrorMessage($"Length ({count}) of Name is Invalid");
    }

    [TestCase(1, "Gaga", "Test")]
    public void ShouldPassWhenNameIsValid(int id, string name, string shortDesc)
    {
        var request = CreateRequest(id, name, shortDesc);
        var result = _validator.TestValidate(request);

        result.ShouldNotHaveValidationErrorFor(product => product.Name);
    }

    [TestCase(1, 513, "Gaga")]
    public void ShouldFailWhenDescriptionLengthIsInvalid(int id, int count, string name)
    {
        var request = CreateRequest(id, name, new string('A', count));
        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(product => product.ShortDesc)
            .WithErrorMessage($"Length ({count}) of Short Desc is Invalid");
    }

    [TestCase(1, "Gaga", "Short description")]
    public void ShouldPassWhenDescriptionIsValid(int id, string name, string shortDesc)
    {
        var request = CreateRequest(id, name, shortDesc);
        var result = _validator.TestValidate(request);

        result.ShouldNotHaveValidationErrorFor(product => product.ShortDesc);
    }
}
