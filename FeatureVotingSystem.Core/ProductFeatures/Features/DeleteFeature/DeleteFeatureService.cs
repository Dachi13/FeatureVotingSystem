using FeatureVotingSystem.Core.Shared.Features.GetFeature;
using FeatureVotingSystem.Shared.Entities.Exceptions;

namespace FeatureVotingSystem.Core.ProductFeatures.Features.DeleteFeature;

public class DeleteFeatureService : IDeleteFeatureService
{
    private readonly IDeleteFeatureRepository _deleteFeatureRepository;
    private readonly IGetFeatureRepository _getFeatureRepository;

    public DeleteFeatureService(IDeleteFeatureRepository deleteFeatureRepository, IGetFeatureRepository getFeatureRepository)
    {
        _deleteFeatureRepository = deleteFeatureRepository;
        _getFeatureRepository = getFeatureRepository;
    }

    public async Task<int> ByIdAsync(int featureId, int userId)
    {
        var userFeature = await _getFeatureRepository.GetByIdAsync(featureId);

        if (userFeature is null || userFeature.StatusId == 3) throw new FeatureNotFoundException();

        var featureOwner = userFeature.UserId == userId;
        
        if (!featureOwner)
            throw new UnauthorizedAccessException("Only the creator of the feature can delete!");

        return await _deleteFeatureRepository.ByIdAsync(featureId);
    }
}