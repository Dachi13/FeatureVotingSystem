namespace FeatureVotingSystem.Core.Products.Features.UpdateProduct;

public interface IUpdateProductRepository
{
    Task<int> UpdateProductAsync(UpdateProductRequest product);

}
