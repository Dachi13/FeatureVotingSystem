using
    FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.GetRequestedFeaturesByVotesQuantityAndStatus;
using FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.GetRequestedFeaturesQuantity;
using FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.GetRequestedFeaturesVotesQuantity;
using FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.Shared.Models.Requests;
using FeatureVotingSystem.Core.Reports.Features.UserReportRequests.GetUserRequestedFeaturesVotesQuantity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FeatureVotingSystem.Core.Reports.Features.UserReportRequests.GetRequestedFeaturesByStatus;

namespace FeatureVotingSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize("MyApiUserPolicy", AuthenticationSchemes = "Bearer")]
public class ReportsController : ControllerBase
{
    private readonly IGetRequestedFeaturesQuantityService _getRequestedFeaturesQuantityService;
    private readonly IGetRequestedFeaturesVotesQuantityService _getRequestedFeaturesVotesQuantityService;

    private readonly IGetRequestedFeaturesByVotesQuantityAndStatusService
        _getRequestedFeaturesByVotesQuantityAndStatusService;

    private readonly IGetRequestedFeaturesByStatusService _getRequestedFeaturesByStatusService;
    private readonly IGetUserRequestedFeaturesVotesQuantityService _getUserRequestedFeaturesVotesQuantityService;

    public ReportsController(
        IGetRequestedFeaturesQuantityService getRequestedFeaturesQuantityService,
        IGetRequestedFeaturesVotesQuantityService getRequestedFeaturesVotesQuantityService,
        IGetRequestedFeaturesByVotesQuantityAndStatusService getRequestedFeaturesByVotesQuantityAndStatusService,
        IGetRequestedFeaturesByStatusService getRequestedFeaturesByStatusService,
        IGetUserRequestedFeaturesVotesQuantityService getUserRequestedFeaturesVotesQuantityService)
    {
        _getRequestedFeaturesQuantityService = getRequestedFeaturesQuantityService;
        _getRequestedFeaturesVotesQuantityService = getRequestedFeaturesVotesQuantityService;
        _getRequestedFeaturesByVotesQuantityAndStatusService = getRequestedFeaturesByVotesQuantityAndStatusService;
        _getRequestedFeaturesByStatusService = getRequestedFeaturesByStatusService;
        _getUserRequestedFeaturesVotesQuantityService = getUserRequestedFeaturesVotesQuantityService;
    }

    [HttpPost("features-quantity")]
    public async Task<IActionResult> GetFeaturesQuantity(
        [FromBody] ProductAuthorReportRequest productAuthorReportRequest)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        int result =
            await _getRequestedFeaturesQuantityService.GetRequestedFeaturesQuantityAsync(userId,
                productAuthorReportRequest);

        return Ok(result);
    }

    [HttpPost("features-quantity-by-timeframe")]
    public async Task<IActionResult> GetFeaturesQuantity(
        [FromBody] ProductAuthorReportFilteredByTimeFrameRequest productAuthorReportFilteredByTimeFrameRequest)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        int result =
            await _getRequestedFeaturesQuantityService.GetRequestedFeaturesQuantityAsync(userId,
                productAuthorReportFilteredByTimeFrameRequest);

        return Ok(result);
    }

    [HttpPost("features-votes-quantity")]
    public async Task<IActionResult> GetFeaturesVotesQuantity(
        [FromBody] ProductAuthorReportRequest productAuthorReportRequest)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        IEnumerable<FeatureVotesQuantityResponse> result =
            await _getRequestedFeaturesVotesQuantityService.GetRequestedFeaturesVotesQuantityAsync(userId,
                productAuthorReportRequest);

        return Ok(result);
    }

    [HttpPost("features-votes-quantity-by-time-frame")]
    public async Task<IActionResult> GetFeaturesVotesQuantity(
        [FromBody] ProductAuthorReportFilteredByTimeFrameRequest productAuthorReportFilteredByTimeFrameRequest)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        IEnumerable<FeatureVotesQuantityResponse> result =
            await _getRequestedFeaturesVotesQuantityService.GetRequestedFeaturesVotesQuantityAsync(userId,
                productAuthorReportFilteredByTimeFrameRequest);

        return Ok(result);
    }

    [HttpPost("feature-list-by-votes-quantity-and-status")]
    public async Task<IActionResult> GetFeaturesListByVotesQuantityAndStatus(
        [FromBody] ProductAuthorReportRequest productAuthorReportRequest)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        IEnumerable<FeatureListByVotesQuantityAndStatusResponse> result =
            await _getRequestedFeaturesByVotesQuantityAndStatusService.GetRequestedFeaturesByVotesQuantityAndStatusAsync(
                userId, productAuthorReportRequest);

        return Ok(result);
    }

    [HttpPost("feature-list-by-votes-quantity-and-status-by-time-frame")]
    public async Task<IActionResult> GetFeaturesListByVotesQuantityAndStatus(
        [FromBody] ProductAuthorReportFilteredByTimeFrameRequest productAuthorReportFilteredByTimeFrameRequest)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        IEnumerable<FeatureListByVotesQuantityAndStatusResponse> result =
            await _getRequestedFeaturesByVotesQuantityAndStatusService.GetRequestedFeaturesByVotesQuantityAndStatusAsync(
                userId, productAuthorReportFilteredByTimeFrameRequest);

        return Ok(result);
    }

    [HttpGet("feature-list-by-status")]
    public async Task<IActionResult> GetFeaturesListByStatus()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        IEnumerable<FeatureListByStatusResponse> result =
            await _getRequestedFeaturesByStatusService.GetRequestedFeaturesByStatusAsync(userId);

        return Ok(result);
    }

    [HttpPost("feature-list-by-status")]
    public async Task<IActionResult> GetFeaturesListByStatus(
        [FromBody] FeatureListByStatusRequest featureListByStatusRequest)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        IEnumerable<FeatureListByStatusResponse> result =
            await _getRequestedFeaturesByStatusService.GetRequestedFeaturesByStatusAsync(userId,
                featureListByStatusRequest);

        return Ok(result);
    }

    [HttpGet("user-requested-feature-votes-quantity")]
    public async Task<IActionResult> GetUserRequestedFeatureVotesQuantity()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        IEnumerable<UserRequestedFeatureVotesQuantityResponse> result =
            await _getUserRequestedFeaturesVotesQuantityService.GetUserRequestedFeaturesVotesQuantityAsync(userId);

        return Ok(result);
    }

    [HttpPost("user-requested-feature-votes-quantity")]
    public async Task<IActionResult> GetUserRequestedFeatureVotesQuantity(
        [FromBody] UserRequestedFeatureVotesQuantityRequest featureListByStatusRequest)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        IEnumerable<UserRequestedFeatureVotesQuantityResponse> result =
            await _getUserRequestedFeaturesVotesQuantityService.GetUserRequestedFeaturesVotesQuantityAsync(userId,
                featureListByStatusRequest);

        return Ok(result);
    }
}