using FeatureVotingSystem.Core.Products.Features.DeleteProduct;
using FeatureVotingSystem.Core.Shared.Models;

namespace FeatureVotingSystem.Core.Tests.Products.Features.DeleteProduct;

public class FakeDeleteProductRepository : IDeleteProductRepository
{
    public Task<int> SoftDeleteProductAsync(int productId)
    {
        Product? testProduct = FakeDbContext.GetListOfProducts().FirstOrDefault(p => p.Id == productId);

        testProduct!.IsDeleted = 1;

        return Task.FromResult(1);
    }

}
