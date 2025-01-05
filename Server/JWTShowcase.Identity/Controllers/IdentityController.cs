namespace JWTShowcase.Identity.Controllers;

using JWTShowcase.Controllers;
using JWTShowcase.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

public class IdentityController : ApiController
{
    private readonly IIdentityService _identity;
    private readonly ICurrentUserService _currentUser;

    public IdentityController(IIdentityService identity, ICurrentUserService currentUser)
    {
        _identity = identity;
        _currentUser = currentUser;
    }

    [HttpPost]
    [Route(nameof(Register))]
    public async Task<ActionResult<UserOutputModel>> Register(UserInputModel input)
    {
        var result = await _identity.Register(input);
        return result.Succeeded ? await Login(input) : BadRequest(result.Errors);
    }

    [HttpPost]
    [Route(nameof(Login))]
    public async Task<ActionResult<UserOutputModel>> Login(UserInputModel input)
    {
        var result = await _identity.Login(input);
        return result.Succeeded ? result.Data : BadRequest(result.Errors);
    }

    [HttpPut]
    [Authorize]
    [Route(nameof(ChangePassword))]
    public async Task<ActionResult> ChangePassword(ChangePasswordInputModel input)
        => await _identity.ChangePassword(_currentUser.UserId, input);
}