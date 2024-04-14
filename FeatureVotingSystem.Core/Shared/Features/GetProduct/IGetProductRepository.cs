using FeatureVotingSystem.Core.Shared.Models;

namespace FeatureVotingSystem.Core.Shared.Features.GetProduct;

public interface IGetProductRepository
{
    Task<Product?> GetProductAsync(int productId);
}
