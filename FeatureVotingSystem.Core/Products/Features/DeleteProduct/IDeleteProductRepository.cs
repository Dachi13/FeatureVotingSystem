namespace FeatureVotingSystem.Core.Products.Features.DeleteProduct;

public interface IDeleteProductRepository
{
    Task<int> SoftDeleteProductAsync(int productId);
}
