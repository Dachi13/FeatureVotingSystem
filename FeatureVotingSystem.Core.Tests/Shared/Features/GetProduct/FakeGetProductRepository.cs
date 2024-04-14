using FeatureVotingSystem.Core.Shared.Features.GetProduct;
using FeatureVotingSystem.Core.Shared.Models;
using FeatureVotingSystem.Core.Tests.Products;

namespace FeatureVotingSystem.Core.Tests.Shared.Features.GetProduct;

public class FakeGetProductRepository : IGetProductRepository
{
    public Task<Product?> GetProductAsync(int productId)
    {
        var testProduct = FakeDbContext.GetListOfProducts().FirstOrDefault(p => p.Id == productId);

        return Task.FromResult(testProduct)!;
    }
}
