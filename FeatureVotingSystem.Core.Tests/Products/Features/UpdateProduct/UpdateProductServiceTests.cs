using FeatureVotingSystem.Core.Products.Features.UpdateProduct;
using FeatureVotingSystem.Core.Shared.Features.GetProduct;
using FeatureVotingSystem.Core.Tests.Shared.Features.GetProduct;
using FeatureVotingSystem.Shared.Entities.Exceptions;
using Shouldly;

namespace FeatureVotingSystem.Core.Tests.Products.Features.UpdateProduct;

public class UpdateProductServiceTests
{
    private IUpdateProductService _updateProductService;

    [SetUp]
    public void SetUp()
    {
        _updateProductService = new UpdateProductService(new FakeUpdateProductRepository(), new FakeGetProductRepository());
    }

    [Test]
    public async Task UpdateInvalidProduct()
    {
        var request = new UpdateProductRequest(13, "Product updated", "Description updated");

        var userId = 3;

        await Should.ThrowAsync<ProductNotFoundException>(async () =>
        {
            await _updateProductService.UpdateProductAsync(request, userId);
        });
    }

    [Test]
    public async Task UpdateProductByInvalidUser()
    {
        var request = new UpdateProductRequest(1, "Product updated", "Description updated");

        var userId = 3;

        await Should.ThrowAsync<UnauthorizedAccessException>(async () =>
        {
            await _updateProductService.UpdateProductAsync(request, userId);
        });
    }

    [Test]
    public async Task UpdateValidProduct()
    {
        var request = new UpdateProductRequest(3, "Productupdated", "Description updated");

        var userId = 3;

        await Should.NotThrowAsync(async () =>
        {
            await _updateProductService.UpdateProductAsync(request, userId);
        });
    }
}
