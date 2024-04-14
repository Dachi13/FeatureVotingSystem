using FeatureVotingSystem.Core.ProductFeatures.Features.AddVote;
using FeatureVotingSystem.Core.Tests.Shared.Features.Email;
using FeatureVotingSystem.Core.Tests.Shared.Features.GetFeature;
using FeatureVotingSystem.Core.Tests.Shared.Features.GetProduct;
using FeatureVotingSystem.Core.Tests.Shared.Features.GetUser;
using FeatureVotingSystem.Shared.Entities.Exceptions;
using Shouldly;

namespace FeatureVotingSystem.Core.Tests.ProductFeatures.Features.AddVote;

[TestFixture]
public class AddVoteServiceTests
{
    private IAddVoteService _addVoteService;

    [SetUp]
    public void SetUp()
    {
        _addVoteService = new AddVoteService
        (
            new FakeGetFeatureRepository(),
            new FakeGetProductRepository(),
            new FakeVoteRepository(),
            new FakeGetUserByIdRepository(),
            new FakeEmailQueueRepository()
        );
    }

    [Test]
    public async Task ShouldNotThrowErrorWhenValidVote()
    {
        var userId = "2";
        var featureId = 7;

        await Should.NotThrowAsync(async () => await _addVoteService.UpVoteAsync(userId, featureId));
    }

    [Test]
    public async Task ShouldThrowLimitExceededExceptionWhenPlacingVote()
    {
        var userId = "1";
        var featureId = 7;

        await Should.ThrowAsync<LimitExceededException>(async () => await _addVoteService.UpVoteAsync(userId, featureId));
    }

    [TestCase(-2)]
    [TestCase(21)]
    public async Task ShouldThrowFeatureNotFoundExceptionWhenVotingOnNonExistingFeature(int featureId)
    {
        var userId = "1";

        await Should.ThrowAsync<FeatureNotFoundException>(async () => await _addVoteService.UpVoteAsync(userId, featureId));
    }

    [Test]
    public async Task ShouldThrowFeatureBadRequestExceptionWhenVotingTwiceSameValue()
    {
        var featureId = 2;
        var userId = "2";

        await Should.ThrowAsync<FeatureBadRequestException>(async () =>
            await _addVoteService.UpVoteAsync(userId, featureId));
    }
}