namespace JWTShowcase.Services;

public interface ICurrentUserService
{
    string UserId { get; }

    bool IsAdministrator { get; }
}