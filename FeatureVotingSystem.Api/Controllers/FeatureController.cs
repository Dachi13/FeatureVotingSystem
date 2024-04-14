using System.Security.Claims;
using FeatureVotingSystem.Core.ProductFeatures.Features.AddVote;
using FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureProperties;
using FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureStatus;
using FeatureVotingSystem.Core.ProductFeatures.Features.CreateFeature;
using FeatureVotingSystem.Core.ProductFeatures.Features.DeleteFeature;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeatureVotingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize("MyApiUserPolicy", AuthenticationSchemes = "Bearer")]
public class FeatureController : ControllerBase
{
    private readonly ICreateFeatureService _createFeatureService;
    private readonly IDeleteFeatureService _deleteFeatureService;
    private readonly IChangeFeaturePropertiesService _changeFeaturePropertiesService;
    private readonly IChangeFeatureStatusService _changeFeatureStatusService;
    private readonly IAddVoteService _voteService;

    public FeatureController(ICreateFeatureService createFeatureService,
        IDeleteFeatureService deleteFeatureService, IChangeFeaturePropertiesService changeFeaturePropertiesService,
        IChangeFeatureStatusService changeFeatureStatusService, IAddVoteService voteService)
    {
        _createFeatureService = createFeatureService;
        _deleteFeatureService = deleteFeatureService;
        _changeFeaturePropertiesService = changeFeaturePropertiesService;
        _changeFeatureStatusService = changeFeatureStatusService;
        _voteService = voteService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateFeatureRequest feature)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        feature.SetUserId(int.Parse(userId));

        var rowsAffected = await _createFeatureService.CreateAsync(feature);

        return rowsAffected == 0
            ? BadRequest("Feature was not created due to some exception")
            : Ok("Successfully created");
    }

    [HttpDelete("delete/{featureId:int}")]
    public async Task<IActionResult> Delete(int featureId)
    {
        var value = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var userId = int.Parse(value);

        var rowsAffected = await _deleteFeatureService.ByIdAsync(featureId, userId);

        return rowsAffected == 0
            ? BadRequest("Failed to delete")
            : Ok("Successfully deleted");
    }

    [HttpPut("change-properties")]
    public async Task<IActionResult> ChangeProperties(
        [FromBody] ChangeFeaturePropertiesRequest featurePropertiesRequest)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        featurePropertiesRequest.SetUserId(int.Parse(userId));

        var rowsAffected = await _changeFeaturePropertiesService.ChangeAsync(featurePropertiesRequest);

        return rowsAffected == 0
            ? BadRequest("Failed to change")
            : Ok("Successfully changed properties");
    }

    [HttpPut("change-status")]
    public async Task<IActionResult> ChangeStatus([FromBody] ChangeFeatureStatusRequest request)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        request.SetUserId(int.Parse(userId));

        var rowsAffected = await _changeFeatureStatusService.ChangeAsync(request);

        return rowsAffected == 0
            ? BadRequest("Failed to change")
            : Ok("Successfully changed properties");
    }

    [HttpPut("upvote/{featureId:int}")]
    public async Task<IActionResult> UpVote(int featureId)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var rowsAffected = await _voteService.UpVoteAsync(userId, featureId);

        return rowsAffected == 0
            ? BadRequest("Failed to UpVote")
            : Ok("Successfully UpVoted");
    }

    [HttpPut("downvote/{featureId:int}")]
    public async Task<IActionResult> DownVote(int featureId)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var rowsAffected = await _voteService.DownVoteAsync(userId, featureId);

        return rowsAffected == 0
            ? BadRequest("Failed to DownVote")
            : Ok("Successfully DownVoted");
    }
}