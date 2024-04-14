using FeatureVotingSystem.Core.ProductFeatures.Enums;
using FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureStatus;
using FeatureVotingSystem.Core.Tests.Shared.Features.Email;
using FeatureVotingSystem.Core.Tests.Shared.Features.GetFeature;
using FeatureVotingSystem.Core.Tests.Shared.Features.GetProduct;
using FeatureVotingSystem.Core.Tests.Shared.Features.GetUser;
using FeatureVotingSystem.Shared.Entities.Exceptions;
using Shouldly;

namespace FeatureVotingSystem.Core.Tests.ProductFeatures.Features.ChangeFeatureStatus;

public class ChangeFeatureStatusServiceTests
{
    private IChangeFeatureStatusService _service;

    [SetUp]
    public void SetUp()
    {
        _service = new ChangeFeatureStatusService
        (
            new FakeChangeFeatureStatusRepository(),
            new FakeGetFeatureRepository(),
            new FakeGetProductRepository(),
            new FakeGetUserByIdRepository(),
            new FakeEmailQueueRepository()
        );
    }

    [Test]
    public async Task ShouldNotThrowErrorWhenValidRequest()
    {
        var request = new ChangeFeatureStatusRequest
        {
            FeatureId = 1,
            StatusId = (int)Status.InProgress
        };

        request.SetUserId(0);

        await Should.NotThrowAsync(async () => await _service.ChangeAsync(request));
    }

    [Test]
    public async Task ShouldThrowFeatureBadRequestExceptionWhenRequestingInValidChangeStatus()
    {
        var request = new ChangeFeatureStatusRequest
        {
            FeatureId = 1,
            RejectionReason = "kj",
            StatusId = (int)Status.InProgress
        };

        request.SetUserId(1);

        await Should.ThrowAsync<FeatureBadRequestException>(async () =>
            await _service.ChangeAsync(request)
        );
    }

    [Test]
    public async Task ShouldNotThrowExceptionWhenValidRejectionRequest()
    {
        var request = new ChangeFeatureStatusRequest
        {
            FeatureId = 1,
            StatusId = (int)Status.Rejected,
            RejectionReason = "Valid rejection reason"
        };

        request.SetUserId(0);
        
        await Should.NotThrowAsync(async () => await _service.ChangeAsync(request));
    }
    
    [Test]
    public async Task ShouldThrowUnAuthorizedExceptionWhenRequestingInvalidChangeStatus()
    {
        var request = new ChangeFeatureStatusRequest
        {
            FeatureId = 1,
            StatusId = (int)Status.InProgress
        };

        request.SetUserId(2);

        await Should.ThrowAsync<UnauthorizedAccessException>(async () =>
            await _service.ChangeAsync(request)
        );
    }
}