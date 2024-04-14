using FeatureVotingSystem.Core.ProductFeatures.Enums;
using FeatureVotingSystem.Core.ProductFeatures.Features;

namespace FeatureVotingSystem.Core.Tests.Comments;

public static class FakeFeatureDbContext
{
    public static async Task<List<Feature>> GetListOfDummyFeatures()
    {
        var features = new List<Feature>();

        var random = new Random();

        for (var i = 0; i < 10; i++)
        {
            features.Add(new()
            {
                Id = i,
                Description = "Description" + i,
                Name = "Feature" + i,
                ProductId = i,
                StatusId = (int)Status.Active,
                UserId = i,
                UploadDate = DateTime.Now.AddDays(-random.Next(10))
            });
        }

        for (var i = 10; i < 20; i++)
        {
            features.Add(new()
            {
                Id = i,
                Description = "Description" + i,
                Name = "Feature" + i,
                ProductId = 10,
                StatusId = (int)Status.Active,
                UserId = 10,
                UploadDate = DateTime.Now.AddDays(random.Next(1))
            });
        }
        
        for (var i = 20; i < 25; i++)
        {
            features.Add(new()
            {
                Id = i,
                Description = "Description" + i,
                Name = "Feature" + i,
                ProductId = 10,
                StatusId = (int)Status.Deleted,
                UserId = 10,
                UploadDate = DateTime.Now.AddDays(random.Next(1))
            });
        }
        
        for (var i = 25; i < 30; i++)
        {
            features.Add(new()
            {
                Id = i,
                Description = "Description" + i,
                Name = "Feature" + i,
                ProductId = 10,
                StatusId = (int)Status.Completed,
                UserId = 10,
                UploadDate = DateTime.Now.AddDays(random.Next(1))
            });
        }
        
        for (var i = 30; i < 35; i++)
        {
            features.Add(new()
            {
                Id = i,
                Description = "Description" + i,
                Name = "Feature" + i,
                ProductId = 10,
                StatusId = (int)Status.Rejected,
                RejectionReason = "Rejected",
                UserId = 10,
                UploadDate = DateTime.Now.AddDays(random.Next(1))
            });
        }

        return await Task.FromResult(features);
    }
}