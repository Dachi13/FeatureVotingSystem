using FeatureVotingSystem.Core.Products.Features.CreateProduct;
using FeatureVotingSystem.Core.Shared.Models;

namespace FeatureVotingSystem.Core.Tests.Products.Features.CreateProduct
{
    public class FakeCreateProductRepository : ICreateProductRepository
    {
        public Task<int> CreateProductAsync(CreateProductRequest product)
        {
            var testProduct = new Product()
            {
                Id = product.UserId,
                Name = product.Name,
                ShortDesc = product.ShortDesc,
                UserId = product.UserId,
                CreatedAt = DateTime.Now,
            };

            FakeDbContext.GetListOfProducts().Add(testProduct);

            return Task.FromResult(1);
        }

        public Task<int> CheckIfProductWithGivenNameAlreadyExistsAsync(string name)
        {
            int productsWithSpecifiedName = FakeDbContext.GetListOfProducts().Where( p => p.Name.ToLower() == name.ToLower()).Count();

            return Task.FromResult(productsWithSpecifiedName);
        }
    }
}
