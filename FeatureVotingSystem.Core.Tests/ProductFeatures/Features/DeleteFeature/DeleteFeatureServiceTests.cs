using FeatureVotingSystem.Core.ProductFeatures.Features.DeleteFeature;
using FeatureVotingSystem.Core.Tests.Shared.Features.GetFeature;
using FeatureVotingSystem.Shared.Entities.Exceptions;
using Shouldly;

namespace FeatureVotingSystem.Core.Tests.ProductFeatures.Features.DeleteFeature;

[TestFixture]
public class DeleteFeatureServiceTests
{
    private IDeleteFeatureService _service;

    [SetUp]
    public void SetUp()
    {
        _service = new DeleteFeatureService
        (
            new FakeDeleteFeatureRepository(),
            new FakeGetFeatureRepository()
        );
    }
    
    [Test]
    public async Task ShouldThrowBadRequestExceptionWhenTryingToDeleteOtherPersonsFeature()
    {
        var featureId = 1;
        var userId = 12;

        await Should.ThrowAsync<UnauthorizedAccessException>(async () => { await _service.ByIdAsync(featureId, userId); });
    }

    [Test]
    public async Task ShouldThrowFeatureNotFoundExceptionWhenTryingToDeleteNonExistingFeature()
    {
        var featureId = -1;
        var userId = 12;

        await Should.ThrowAsync<FeatureNotFoundException>(async () => { await _service.ByIdAsync(featureId, userId); });
    }

    [Test]
    public async Task ShouldThrowNotFoundExceptionWhenDeletingNotExistingFeature()
    {
        var featureId = -90;
        var userId = 12;

        await Should.ThrowAsync<NotFoundException>(async () => { await _service.ByIdAsync(featureId, userId); });
    }
}