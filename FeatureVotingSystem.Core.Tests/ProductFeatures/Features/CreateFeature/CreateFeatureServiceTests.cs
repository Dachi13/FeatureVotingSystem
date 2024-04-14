using FeatureVotingSystem.Core.ProductFeatures.Features.CreateFeature;
using FeatureVotingSystem.Core.Tests.Shared.Features.Email;
using FeatureVotingSystem.Core.Tests.Shared.Features.GetProduct;
using FeatureVotingSystem.Core.Tests.Shared.Features.GetUser;
using FeatureVotingSystem.Shared.Entities.Exceptions;
using Shouldly;

namespace FeatureVotingSystem.Core.Tests.ProductFeatures.Features.CreateFeature;

[TestFixture]
public class CreateFeatureServiceTests
{
    private ICreateFeatureService _service;

    [SetUp]
    public void SetUp()
    {
        _service = new CreateFeatureService
        (
            new FakeCreateFeatureRepository(),
            new FakeGetProductRepository(),
            new FakeGetUserByIdRepository(),
            new FakeEmailQueueRepository()
        );
    }

    [Test]
    public async Task ShouldReturn1AsAffectedRowsWhenAddingValidFeature()
    {
        var request = new CreateFeatureRequest
        {
            Name = "ValidName",
            Description = "Description",
            ProductId = 7
        };

        request.SetUserId(2);

        var affectedRows = await _service.CreateAsync(request);

        affectedRows.ShouldBe(1);
    }


    [Test]
    public async Task ShouldThrowLimitExceededExceptionWhenAddingFeature()
    {
        var userId = 11;
        var productId = 10;

        var request = new CreateFeatureRequest
        {
            Name = "ValidName",
            Description = "Description",
            ProductId = productId
        };

        request.SetUserId(userId);

        await Should.ThrowAsync<LimitExceededException>(async () => { await _service.CreateAsync(request); });
    }

    [Test]
    public async Task ShouldThrowExceptionWhenAddingInvalidFeature()
    {
        var userId = 12;
        var productId = 10;

        var request = new CreateFeatureRequest
        {
            Name = new string('A', 560),
            Description = "Description",
            ProductId = productId
        };

        await Should.ThrowAsync<FeatureBadRequestException>(async () => { await _service.CreateAsync(request); });
    }
}