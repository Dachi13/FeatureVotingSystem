namespace FeatureVotingSystem.Core.Products.Features.CreateProduct;

public interface ICreateProductService
{
    Task CreateProductAsync(CreateProductRequest product, int userId);
}
