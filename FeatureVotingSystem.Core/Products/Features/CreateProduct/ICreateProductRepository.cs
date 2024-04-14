namespace FeatureVotingSystem.Core.Products.Features.CreateProduct;

public interface ICreateProductRepository
{
    Task<int> CreateProductAsync(CreateProductRequest product);
    Task<int> CheckIfProductWithGivenNameAlreadyExistsAsync(string name);
}
