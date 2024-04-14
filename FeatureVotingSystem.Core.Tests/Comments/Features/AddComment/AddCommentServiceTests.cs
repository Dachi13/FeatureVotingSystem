using FeatureVotingSystem.Core.Comments.Features.AddComment;
using FeatureVotingSystem.Core.Tests.Shared.Features.Email;
using FeatureVotingSystem.Core.Tests.Shared.Features.GetFeature;
using FeatureVotingSystem.Core.Tests.Shared.Features.GetProduct;
using FeatureVotingSystem.Core.Tests.Shared.Features.GetUser;
using FeatureVotingSystem.Shared.Entities.Exceptions;
using Shouldly;

namespace FeatureVotingSystem.Core.Tests.Comments.Features.AddComment;

[TestFixture]
public class AddCommentServiceTests
{
    private IAddCommentService _addCommentService;

    [SetUp]
    public void SetUp()
    {
        _addCommentService = new AddCommentService(
            new FakeAddCommentRepository(),
            new FakeGetFeatureRepository(),
            new FakeGetProductRepository(),
            new FakeGetUserByIdRepository(),
            new FakeEmailQueueRepository()
        );
    }

    [Test]
    public async Task ShouldNotThrowErrorWhenAddingValidComment()
    {
        var request = new AddCommentRequest
        {
            Text = "ValidComment",
            FeatureId = 2
        };

        request.SetUserId(1);
        await Should.NotThrowAsync(async () => await _addCommentService.AddCommentAsync(request));
    }

    [Test]
    public void ShouldThrowCommentBadRequestWhenRequestFails()
    {
        var request = new AddCommentRequest
        {
            Text = new string('A', 550),
            FeatureId = 2
        };

        request.SetUserId(1);

        Should.Throw<CommentBadRequestException>(async () => { await _addCommentService.AddCommentAsync(request); });
    }

    [Test]
    public void ShouldThrowCommentBadRequestWhenCommentIsTooShort()
    {
        var request = new AddCommentRequest
        {
            Text = "",
            FeatureId = 2
        };

        request.SetUserId(1);

        Should.Throw<CommentBadRequestException>(async () => { await _addCommentService.AddCommentAsync(request); });
    }

    [TestCase(-2)]
    [TestCase(20)]
    public void ShouldThrowFeatureNotFoundExceptionWhenCommentingOnNonExistingFeature(int featureId)
    {
        var request = new AddCommentRequest
        {
            Text = "Comment",
            FeatureId = featureId
        };

        request.SetUserId(1);

        Should.Throw<FeatureNotFoundException>(async () => { await _addCommentService.AddCommentAsync(request); });
    }

    [TestCase(25)]
    [TestCase(30)]
    public void ShouldThrowFeatureBadRequestExceptionWhenFeatureStatusIsCompletedOrRejected(int featureId)
    {
        var request = new AddCommentRequest
        {
            Text = "Comment",
            FeatureId = featureId
        };

        request.SetUserId(1);

        Should.Throw<FeatureBadRequestException>(async () => { await _addCommentService.AddCommentAsync(request); });
    }
}