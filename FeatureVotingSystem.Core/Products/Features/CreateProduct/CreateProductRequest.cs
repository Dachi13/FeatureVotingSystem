using FeatureVotingSystem.Shared.Entities.Exceptions;

namespace FeatureVotingSystem.Core.Products.Features.CreateProduct;

public class CreateProductRequest
{
    public string Name { get; set; }
    public string ShortDesc { get; set; }
    public int UserId { get; private set; }
    public DateTime CreatedAt { get; } = DateTime.Now;

    public void SetUserId(int userId)
    {
        UserId = userId;
        Validate();
    }

    private void Validate()
    {
        var validationResult = ProductValidations.ValidateCreateProductRequest(this);

        if (!validationResult.IsValid)
            throw new ProductBadRequestException(validationResult.Errors.First().ErrorMessage);
    }
}