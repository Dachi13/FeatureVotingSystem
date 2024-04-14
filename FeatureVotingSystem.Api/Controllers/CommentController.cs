using System.Security.Claims;
using FeatureVotingSystem.Core.Comments.Features.AddComment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeatureVotingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize("MyApiUserPolicy", AuthenticationSchemes = "Bearer")]
public class CommentController : ControllerBase
{
    private readonly IAddCommentService _addCommentService;

    public CommentController(IAddCommentService addCommentService)
    {
        _addCommentService = addCommentService;
    }

    [HttpPost("add-comment")]
    public async Task<ActionResult> AddComment([FromBody] AddCommentRequest request)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        request.SetUserId(int.Parse(userId));

        var affectedRows = await _addCommentService.AddCommentAsync(request);

        return affectedRows == 0
            ? BadRequest("Couldn't add Comment")
            : Ok("Successfully added comment");
    }
}