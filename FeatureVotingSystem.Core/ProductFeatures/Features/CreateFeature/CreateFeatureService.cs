using FeatureVotingSystem.Core.ProductFeatures.Enums;
using FeatureVotingSystem.Core.Shared.Features.GetProduct;
using FeatureVotingSystem.Core.Shared.Features.GetUser;
using FeatureVotingSystem.Shared.Entities.Exceptions;
using FeatureVotingSystem.Shared.Features;
using FeatureVotingSystem.Shared.Features.AddEmailInQueue;

namespace FeatureVotingSystem.Core.ProductFeatures.Features.CreateFeature;

public class CreateFeatureService : ICreateFeatureService
{
    private readonly ICreateFeatureRepository _createFeatureRepository;
    private readonly IGetProductRepository _getProductRepository;
    private readonly IGetUserByIdRepository _getUserByIdRepository;
    private readonly IAddEmailInQueueRepository _addEmailInQueueRepository;

    public CreateFeatureService(ICreateFeatureRepository createFeatureRepository,
        IGetProductRepository getProductRepository, IGetUserByIdRepository getUserByIdRepository,
        IAddEmailInQueueRepository addEmailInQueueRepository)
    {
        _createFeatureRepository = createFeatureRepository;
        _getProductRepository = getProductRepository;
        _getUserByIdRepository = getUserByIdRepository;
        _addEmailInQueueRepository = addEmailInQueueRepository;
    }

    public async Task<int> CreateAsync(CreateFeatureRequest request)
    {
        var userIsAbleToPlaceNewFeature = await IsUserAbleToPlaceNewFeature(request);

        if (!userIsAbleToPlaceNewFeature)
            throw new LimitExceededException("You can register functionality only 10 times per day");

        var product = await _getProductRepository.GetProductAsync(request.ProductId);

        if (product is null || product.IsDeleted == 1) throw new ProductNotFoundException(request.ProductId);

        var affectedRows = await _createFeatureRepository.CreateAsync(request);

        var productOwner = (await _getUserByIdRepository.FindByIdAsync(product.UserId.ToString()))!;

        var emailMessage = $"Under {product.Name} new feature '{request.Name}' has been uploaded";

        await _addEmailInQueueRepository.AddEmailInQueueAsync(productOwner.Id, (int)EmailSubject.FeatureRequest, emailMessage);

        return affectedRows;
    }

    private async Task<bool> IsUserAbleToPlaceNewFeature(CreateFeatureRequest request)
    {
        var totalFeaturesOnProductByUser = await _createFeatureRepository.GetTotalFeaturesUploadedByUserSinceDateAsync(
            request.UserId,
            request.ProductId,
            DateTime.Now.AddDays(-(int)FeatureMagicNumber.FeaturePostingLimitPeriod));

        return totalFeaturesOnProductByUser < (int)FeatureMagicNumber.FeaturePostingLimit;
    }
}