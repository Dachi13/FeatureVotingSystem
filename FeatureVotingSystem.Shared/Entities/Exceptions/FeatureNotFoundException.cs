namespace FeatureVotingSystem.Shared.Entities.Exceptions;

public class FeatureNotFoundException : NotFoundException
{
    public FeatureNotFoundException() : base("Such feature wan't found")
    {
    }
}