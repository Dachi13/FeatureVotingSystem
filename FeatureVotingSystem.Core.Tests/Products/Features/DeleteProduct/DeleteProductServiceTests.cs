using FeatureVotingSystem.Core.Products.Features.DeleteProduct;
using FeatureVotingSystem.Core.Tests.Shared.Features.GetProduct;
using FeatureVotingSystem.Shared.Entities.Exceptions;
using Shouldly;

namespace FeatureVotingSystem.Core.Tests.Products.Features.DeleteProduct;

public class DeleteProductServiceTests
{
    private IDeleteProductService _deleteProductService;

    [SetUp]
    public void SetUp()
    {
        _deleteProductService = new DeleteProductService(new FakeDeleteProductRepository(), new FakeGetProductRepository());
    }

    [Test]
    public async Task DeleteInvalidProduct()
    {
        var userId = 3;

        await Should.ThrowAsync<ProductNotFoundException>(async () =>
        {
            await _deleteProductService.SoftDeleteProductAsync(13, userId);
        });
    }

    [Test]
    public async Task DeleteProductByInvalidUser()
    {
        var userId = 4;

        await Should.ThrowAsync<UnauthorizedAccessException>(async () =>
        {
            await _deleteProductService.SoftDeleteProductAsync(5, userId);
        });
    }

    [Test]
    public async Task DeleteValidProduct()
    {
        var userId = 4;

        await Should.NotThrowAsync(async () =>
        {
            await _deleteProductService.SoftDeleteProductAsync(4, userId);
        });
    }
}
