using FeatureVotingSystem.Core.Products.Features.UpdateProduct;
using FeatureVotingSystem.Core.Shared.Models;

namespace FeatureVotingSystem.Core.Tests.Products.Features.UpdateProduct;

public class FakeUpdateProductRepository : IUpdateProductRepository
{
    public Task<int> UpdateProductAsync(UpdateProductRequest product)
    {
        Product? productToUpdate = FakeDbContext.GetListOfProducts().FirstOrDefault(p => p.Id == product.Id);

        productToUpdate!.Name = product.Name;
        productToUpdate.ShortDesc = product.ShortDesc;

        return Task.FromResult(1);
    }
}
