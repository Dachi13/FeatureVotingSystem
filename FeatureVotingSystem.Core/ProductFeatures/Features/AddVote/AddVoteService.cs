using FeatureVotingSystem.Core.ProductFeatures.Enums;
using FeatureVotingSystem.Core.Shared.Features.GetFeature;
using FeatureVotingSystem.Core.Shared.Features.GetProduct;
using FeatureVotingSystem.Core.Shared.Features.GetUser;
using FeatureVotingSystem.Shared.Entities.Exceptions;
using FeatureVotingSystem.Shared.Features;
using FeatureVotingSystem.Shared.Features.AddEmailInQueue;

namespace FeatureVotingSystem.Core.ProductFeatures.Features.AddVote;

public class AddVoteService : IAddVoteService
{
    private readonly IGetFeatureRepository _getFeatureRepository;
    private readonly IGetProductRepository _getProductRepository;
    private readonly IVoteRepository _voteRepository;
    private readonly IGetUserByIdRepository _getUserByIdRepository;
    private readonly IAddEmailInQueueRepository _addEmailInQueueRepository;

    public AddVoteService(IGetFeatureRepository getFeatureRepository, IGetProductRepository getProductRepository,
        IVoteRepository voteRepository, IGetUserByIdRepository getUserByIdRepository,
        IAddEmailInQueueRepository addEmailInQueueRepository)
    {
        _getFeatureRepository = getFeatureRepository;
        _getProductRepository = getProductRepository;
        _voteRepository = voteRepository;
        _getUserByIdRepository = getUserByIdRepository;
        _addEmailInQueueRepository = addEmailInQueueRepository;
    }

    public async Task<int> UpVoteAsync(string userId, int featureId)
    {
        var request = new VoteRequest
        {
            FeatureId = featureId
        };
        request.SetVoteValue(1);
        request.SetUserId(int.Parse(userId));

        return await Vote(request);
    }

    public async Task<int> DownVoteAsync(string userId, int featureId)
    {
        var request = new VoteRequest
        {
            FeatureId = featureId
        };
        request.SetVoteValue(-1);
        request.SetUserId(int.Parse(userId));

        return await Vote(request);
    }

    private async Task<int> Vote(VoteRequest voteRequest)
    {
        var feature = await _getFeatureRepository.GetByIdAsync(voteRequest.FeatureId);

        if (feature is null || feature.StatusId == (int)Status.Deleted) throw new FeatureNotFoundException();

        voteRequest.SetProductId(feature.ProductId);

        var vote = await _voteRepository.GetVoteOnFeatureByUserAsync(voteRequest.UserId, voteRequest.FeatureId);

        if (vote is not null) return await UpdateVote(voteRequest, vote);
        
        var userIsAbleToVote = await IsUserAbleToVote(voteRequest);

        if (!userIsAbleToVote) throw new LimitExceededException("You can only place vote 3 times per 7 days");

        var affectedRows = await _voteRepository.AddVoteAsync(voteRequest);

        var product = (await _getProductRepository.GetProductAsync(feature.ProductId))!;

        var productOwner = (await _getUserByIdRepository.FindByIdAsync(product.UserId.ToString()))!;
        var nameOfTheUserWhoVoted = (await _getUserByIdRepository.FindByIdAsync(voteRequest.UserId.ToString()))!;
        var featureOwner = (await _getUserByIdRepository.FindByIdAsync(feature.UserId.ToString()))!;

        var emailMessage =
            $"On {product.Name} under {feature.Name} feature, {nameOfTheUserWhoVoted.UserName} placed a vote";

        await _addEmailInQueueRepository.AddEmailInQueueAsync(productOwner.Id, (int)EmailSubject.FeatureVote, emailMessage);
        await _addEmailInQueueRepository.AddEmailInQueueAsync(featureOwner.Id, (int)EmailSubject.FeatureVote, emailMessage);

        return affectedRows;
    }

    private async Task<int> UpdateVote(VoteRequest request, Vote vote)
    {
        var sameVote = request.VoteValue == vote.VoteValue;

        return sameVote
            ? throw new FeatureBadRequestException("You have already voted")
            : await _voteRepository.UpdateVoteAsync(request);
    }

    private async Task<bool> IsUserAbleToVote(VoteRequest voteRequest)
    {
        var totalUserVotesOnProduct = await _voteRepository.TotalUserVotesOnProductAsync(
            voteRequest.UserId,
            voteRequest.ProductId,
            DateTime.Now.AddDays(-(int)FeatureMagicNumber.VotePostingLimitPeriod));

        return totalUserVotesOnProduct < (int)FeatureMagicNumber.VoteLimit;
    }
}