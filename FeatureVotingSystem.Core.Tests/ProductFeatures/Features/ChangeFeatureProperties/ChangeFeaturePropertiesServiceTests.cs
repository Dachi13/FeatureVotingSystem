using FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureProperties;
using FeatureVotingSystem.Core.Tests.Shared.Features.Email;
using FeatureVotingSystem.Core.Tests.Shared.Features.GetFeature;
using FeatureVotingSystem.Core.Tests.Shared.Features.GetProduct;
using FeatureVotingSystem.Core.Tests.Shared.Features.GetUser;
using FeatureVotingSystem.Shared.Entities.Exceptions;
using Shouldly;

namespace FeatureVotingSystem.Core.Tests.ProductFeatures.Features.ChangeFeatureProperties;

[TestFixture]
public class ChangeFeaturePropertiesServiceTests
{
    private IChangeFeaturePropertiesService _service;

    [SetUp]
    public void SetUp()
    {
        _service = new ChangeFeaturePropertiesService
        (
            new FakeChangeFeaturePropertiesRepository(),
            new FakeGetFeatureRepository(),
            new FakeGetProductRepository(),
            new FakeGetUserByIdRepository(),
            new FakeEmailQueueRepository()
        );
    }

    [Test]
    public async Task ShouldNotThrowErrorWhenAddingValidRequest()
    {
        var request = new ChangeFeaturePropertiesRequest
        {
            Id = 1,
            Name = "Changed",
            Description = "Changed",
        };

        request.SetUserId(1);

        await Should.NotThrowAsync(async () => await _service.ChangeAsync(request));
    }

    [Test]
    public async Task ShouldThrowFeatureFeatureBadRequestExceptionWhenChangeFeatureRequestIsInvalid()
    {
        var userId = 12;
        var productId = 10;

        var request = new ChangeFeaturePropertiesRequest
        {
            Name = new string('A', 560),
            Description = "Description",
            Id = userId
        };

        request.SetUserId(12);

        await Should.ThrowAsync<FeatureBadRequestException>(async () => { await _service.ChangeAsync(request); });
    }

    [TestCase(-2)]
    [TestCase(20)]
    public async Task ShouldThrowNotFoundExceptionWhenChangingNotExistingFeature(int featureId)
    {
        var request = new ChangeFeaturePropertiesRequest
        {
            Id = featureId,
            Name = "Changed",
            Description = "Changed",
        };

        request.SetUserId(1);

        await Should.ThrowAsync<FeatureNotFoundException>(async () => { await _service.ChangeAsync(request); });
    }

    [Test]
    public async Task ShouldThrowUnAuthorizedExceptionWhenUserChangesOtherUsersFeaturesProperty()
    {
        var request = new ChangeFeaturePropertiesRequest
        {
            Id = 1,
            Name = "Changed",
            Description = "Changed",
        };

        request.SetUserId(2);

        await Should.ThrowAsync<UnauthorizedAccessException>(async () => { await _service.ChangeAsync(request); });
    }
}