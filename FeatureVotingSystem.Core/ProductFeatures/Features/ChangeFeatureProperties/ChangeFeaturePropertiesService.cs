using FeatureVotingSystem.Core.ProductFeatures.Enums;
using FeatureVotingSystem.Core.Shared.Features.GetFeature;
using FeatureVotingSystem.Core.Shared.Features.GetProduct;
using FeatureVotingSystem.Core.Shared.Features.GetUser;
using FeatureVotingSystem.Shared.Entities.Exceptions;
using FeatureVotingSystem.Shared.Features;
using FeatureVotingSystem.Shared.Features.AddEmailInQueue;

namespace FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureProperties;

public class ChangeFeaturePropertiesService : IChangeFeaturePropertiesService
{
    private readonly IChangeFeaturePropertiesRepository _changeRepository;
    private readonly IGetFeatureRepository _getFeature;
    private readonly IGetProductRepository _getProductRepository;
    private readonly IGetUserByIdRepository _getUserByIdRepository;
    private readonly IAddEmailInQueueRepository _addEmailInQueueRepository;

    public ChangeFeaturePropertiesService(IChangeFeaturePropertiesRepository changeRepository,
        IGetFeatureRepository getFeature, IGetProductRepository getProductRepository,
        IGetUserByIdRepository getUserByIdRepository, IAddEmailInQueueRepository addEmailInQueueRepository)
    {
        _changeRepository = changeRepository;
        _getFeature = getFeature;
        _getProductRepository = getProductRepository;
        _getUserByIdRepository = getUserByIdRepository;
        _addEmailInQueueRepository = addEmailInQueueRepository;
    }

    public async Task<int> ChangeAsync(ChangeFeaturePropertiesRequest featurePropertiesRequest)
    {
        var feature = await _getFeature.GetByIdAsync(featurePropertiesRequest.Id);

        if (feature is null || feature.StatusId == (int)Status.Deleted) throw new FeatureNotFoundException();

        var featureOwner = feature.UserId == featurePropertiesRequest.UserId;
        
        if (!featureOwner)
            throw new UnauthorizedAccessException("You're not the creator of the feature");

        var affectedRows = await _changeRepository.ChangeAsync(featurePropertiesRequest);

        var product = (await _getProductRepository.GetProductAsync(feature.ProductId))!;

        var userUpdatesFeatureDescription = !feature.Description.Equals(featurePropertiesRequest.Description);

        if (!userUpdatesFeatureDescription) return affectedRows;

        var productOwner = (await _getUserByIdRepository.FindByIdAsync(product.UserId.ToString()))!;

        var emailMessage = $"Under {product.Name} feature '{feature.Name}' was updated";

        await _addEmailInQueueRepository.AddEmailInQueueAsync(productOwner.Id, (int)EmailSubject.FeatureDescriptionChange,
            emailMessage);

        return affectedRows;
    }
}