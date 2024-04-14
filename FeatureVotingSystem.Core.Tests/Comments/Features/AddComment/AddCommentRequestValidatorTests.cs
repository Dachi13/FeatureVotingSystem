using FeatureVotingSystem.Core.Comments.Features.AddComment;
using FluentValidation.TestHelper;

namespace FeatureVotingSystem.Core.Tests.Comments.Features.AddComment;

[TestFixture]
public class AddCommentRequestValidatorTests
{
    private AddCommentValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new AddCommentValidator();
    }

    [Test]
    public void ShouldPassWhenRequestingValidComment()
    {
        var request = new AddCommentRequest
        {
            Text = "comment",
            FeatureId = 1
        };
        
        request.SetUserId(1);

        var result = _validator.TestValidate(request);

        result.ShouldNotHaveValidationErrorFor(re => re.Text);
    }
    
    [Test]
    public void ShouldThrowErrorWhenRequestingLongComment()
    {
        var request = new AddCommentRequest
        {
            Text = new string('A',560)
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(re => re.Text);
    }
    
    [Test]
    public void ShouldThrowErrorWhenRequestingShortComment()
    {
        var request = new AddCommentRequest
        {
            Text = ""
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(re => re.Text);
    }
    
    [Test]
    public void ShouldThrowErrorWhenCommentIsNull()
    {
        var request = new AddCommentRequest();

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(re => re.Text);
    }
}