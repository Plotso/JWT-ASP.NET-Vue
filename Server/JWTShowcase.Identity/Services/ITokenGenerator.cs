namespace JWTShowcase.Identity.Services;

using Data.Models;

public interface ITokenGenerator
{
    string GenerateToken(User user, IEnumerable<string> roles = null);
}