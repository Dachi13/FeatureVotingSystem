using FeatureVotingSystem.Api.Auth;
using FeatureVotingSystem.Core.Users.Features.LoginUser;
using FeatureVotingSystem.Core.Users.Features.RegisterUser;
using Microsoft.AspNetCore.Mvc;

namespace FeatureVotingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly JwtTokenGenerator _tokenGenerator;
    private readonly IRegisterUserService _registerUser;
    private readonly ILoginUserService _loginUser;

    public UserController(JwtTokenGenerator tokenGenerator, IRegisterUserService registerUser,
        ILoginUserService loginUser)
    {
        _tokenGenerator = tokenGenerator;
        _registerUser = registerUser;
        _loginUser = loginUser;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        await _registerUser.RegisterAsync(request);

        return Ok("You've successfully registered");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var userId = await _loginUser.LoginAsync(request);
        var jwt = _tokenGenerator.Generate(userId.ToString());

        return Ok(jwt);
    }
}