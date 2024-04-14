using FluentValidation;

namespace FeatureVotingSystem.Core.ProductFeatures.Features.AddVote;

public class VoteRequestValidator : AbstractValidator<VoteRequest>
{
    public VoteRequestValidator()
    {
        RuleFor(vote => vote.VoteValue)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Must(votedValue => votedValue is 1 or -1).WithMessage("{PropertyName} contains invalid characters");
    }
}