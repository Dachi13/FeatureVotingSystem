using FeatureVotingSystem.Core.ProductFeatures.Enums;
using FeatureVotingSystem.Core.Shared.Features.GetFeature;
using FeatureVotingSystem.Core.Shared.Features.GetProduct;
using FeatureVotingSystem.Core.Shared.Features.GetUser;
using FeatureVotingSystem.Shared.Entities.Exceptions;
using FeatureVotingSystem.Shared.Features;
using FeatureVotingSystem.Shared.Features.AddEmailInQueue;

namespace FeatureVotingSystem.Core.Comments.Features.AddComment;

public class AddCommentService : IAddCommentService
{
    private readonly IAddCommentRepository _addCommentRepository;
    private readonly IGetFeatureRepository _getFeatureRepository;
    private readonly IGetProductRepository _getProductRepository;
    private readonly IGetUserByIdRepository _getUserByIdRepository;
    private readonly IAddEmailInQueueRepository _addEmailInQueueRepository;

    public AddCommentService(
        IAddCommentRepository addCommentRepository,
        IGetFeatureRepository getFeatureRepository,
        IGetProductRepository getProductRepository,
        IGetUserByIdRepository getUserRepository,
        IAddEmailInQueueRepository addEmailInQueueRepository)
    {
        _addCommentRepository = addCommentRepository;
        _getFeatureRepository = getFeatureRepository;
        _getProductRepository = getProductRepository;
        _getUserByIdRepository = getUserRepository;
        _addEmailInQueueRepository = addEmailInQueueRepository;
    }

    public async Task<int> AddCommentAsync(AddCommentRequest request)
    {
        var validationResult = CommentValidations.ValidateCommentRequest(request);

        if (!validationResult.IsValid)
            throw new CommentBadRequestException(validationResult.Errors.First().ErrorMessage);

        var feature = await _getFeatureRepository.GetByIdAsync(request.FeatureId);

        if (feature is null || feature.StatusId == (int)Status.Deleted) throw new FeatureNotFoundException();

        if (feature.StatusId > (int)Status.InProgress)
            throw new FeatureBadRequestException("Feature is either Completed or Rejected");

        var affectedRows = await _addCommentRepository.AddAsync(request);

        var product = (await _getProductRepository.GetProductAsync(feature.ProductId))!;
        var productOwner = (await _getUserByIdRepository.FindByIdAsync(product.UserId.ToString()))!;
        var featureOwner = (await _getUserByIdRepository.FindByIdAsync(feature.UserId.ToString()))!;

        var emailMessage = $"On product '{product.Name}', under feature '{feature.Name}' someone added a comment";

        await _addEmailInQueueRepository.AddEmailInQueueAsync(productOwner.Id, (int)EmailSubject.FeatureComment,
            emailMessage);

        await _addEmailInQueueRepository.AddEmailInQueueAsync(featureOwner.Id, (int)EmailSubject.FeatureComment,
            emailMessage);

        return affectedRows;
    }
}