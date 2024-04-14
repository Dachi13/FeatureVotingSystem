using FeatureVotingSystem.Core.ProductFeatures.Enums;
using FeatureVotingSystem.Core.Shared.Features.GetFeature;
using FeatureVotingSystem.Core.Shared.Features.GetProduct;
using FeatureVotingSystem.Core.Shared.Features.GetUser;
using FeatureVotingSystem.Shared.Entities.Exceptions;
using FeatureVotingSystem.Shared.Features;
using FeatureVotingSystem.Shared.Features.AddEmailInQueue;

namespace FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureStatus;

public class ChangeFeatureStatusService : IChangeFeatureStatusService
{
    private readonly IChangeFeatureStatusRepository _changeFeatureStatusRepository;
    private readonly IGetFeatureRepository _getFeatureRepository;
    private readonly IGetProductRepository _getProductRepository;
    private readonly IGetUserByIdRepository _getUserByIdRepository;
    private readonly IAddEmailInQueueRepository _addEmailInQueueRepository;

    public ChangeFeatureStatusService(IChangeFeatureStatusRepository changeFeatureStatusRepository,
        IGetFeatureRepository getFeatureRepository, IGetProductRepository getProductRepository,
        IGetUserByIdRepository getUserByIdRepository, IAddEmailInQueueRepository addEmailInQueueRepository)
    {
        _changeFeatureStatusRepository = changeFeatureStatusRepository;
        _getFeatureRepository = getFeatureRepository;
        _getProductRepository = getProductRepository;
        _getUserByIdRepository = getUserByIdRepository;
        _addEmailInQueueRepository = addEmailInQueueRepository;
    }

    public async Task<int> ChangeAsync(ChangeFeatureStatusRequest request)
    {
        var validationResult = FeatureValidations.ValidateChangeFeatureStatusRequest(request);

        if (!validationResult.IsValid)
            throw new FeatureBadRequestException(validationResult.Errors.First().ErrorMessage);

        var feature = await _getFeatureRepository.GetByIdAsync(request.FeatureId);

        if (feature is null || feature.StatusId == (int)Status.Deleted) throw new FeatureNotFoundException();

        var product = (await _getProductRepository.GetProductAsync(feature.ProductId))!;
        var productOwner = (await _getUserByIdRepository.FindByIdAsync(product.UserId.ToString()))!;

        var productOwnerChangesStatus = productOwner.Id == request.UserId;

        if (!productOwnerChangesStatus)
            throw new UnauthorizedAccessException("Only owner of the product can change it's features status");

        var affectedRows = await _changeFeatureStatusRepository.ChangeAsync(request);

        var featureOwner = (await _getUserByIdRepository.FindByIdAsync(feature.UserId.ToString()))!;

        var emailMessage = $"Under {product.Name} on feature '{feature.Name}' status has been changed by the owner";
        
        await _addEmailInQueueRepository.AddEmailInQueueAsync(featureOwner.Id, (int)EmailSubject.FeatureStatusChange,
            emailMessage);

        return affectedRows;
    }
}