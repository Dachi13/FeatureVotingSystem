namespace FeatureVotingSystem.Core.Products.Features.DeleteProduct;

public interface IDeleteProductService
{
    Task SoftDeleteProductAsync(int productId, int userId);
}
