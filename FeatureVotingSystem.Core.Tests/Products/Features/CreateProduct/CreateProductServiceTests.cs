using FeatureVotingSystem.Core.Products.Features.CreateProduct;
using FeatureVotingSystem.Shared.Entities.Exceptions;
using Shouldly;

namespace FeatureVotingSystem.Core.Tests.Products.Features.CreateProduct;

public class CreateProductServiceTests
{
    private ICreateProductService _createProductService;

    [SetUp]
    public void SetUp()
    {
        _createProductService = new CreateProductService(new FakeCreateProductRepository());
    }

    [Test]
    public async Task CreateValidProduct()
    {
        var request = new CreateProductRequest()
        {
            Name = "product 11",
            ShortDesc = "Short Description 11"
        };

        var userId = 11;

        await Should.NotThrowAsync(async () =>
        {
            await _createProductService.CreateProductAsync(request, userId);
        });
    }

    [Test]
    public async Task CreateInvalidProduct()
    {
        CreateProductRequest request = null;

        var userId = 3;

        await Should.ThrowAsync<ProductBadRequestException>(async () =>
        {
            await _createProductService.CreateProductAsync(request, userId);
        });
    }
}
