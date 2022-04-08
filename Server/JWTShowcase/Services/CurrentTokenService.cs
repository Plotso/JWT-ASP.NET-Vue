namespace JWTShowcase.Services;
public class CurrentTokenService : ICurrentTokenService
{
    private string _token;

    public string Get() => _token;

    public void Set(string token) => _token = token;
}
