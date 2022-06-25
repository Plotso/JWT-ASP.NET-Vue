namespace JWTShowcase.Identity.Models;

public record UserOutputModel(string Token, string Email, bool HasAdministrativeRights);