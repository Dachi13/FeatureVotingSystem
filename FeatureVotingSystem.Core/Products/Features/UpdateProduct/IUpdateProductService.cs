namespace FeatureVotingSystem.Core.Products.Features.UpdateProduct;

public interface IUpdateProductService
{
    Task UpdateProductAsync(UpdateProductRequest product, int userId);
}
