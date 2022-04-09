namespace JWTShowcase.Services;

using System.Security.Claims;
using Microsoft.AspNetCore.Http;

public class CurrentUserService : ICurrentUserService
{
    private readonly ClaimsPrincipal _user;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _user = httpContextAccessor.HttpContext?.User;

        if (_user == null)
            throw new InvalidOperationException("Request requires an authenticated user.");

        UserId = _user.FindFirstValue(ClaimTypes.NameIdentifier);
        Username = _user.Identity?.Name;
    }
    
    public string UserId { get; }
    
    public string? Username { get; set; }
    
    public bool IsAdministrator => _user.IsInRole(Constants.AdministratorRoleName);
}