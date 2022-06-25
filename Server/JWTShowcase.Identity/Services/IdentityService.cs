namespace JWTShowcase.Identity.Services;

using Data.Models;
using JWTShowcase.Models;
using Microsoft.AspNetCore.Identity;
using Models;

class IdentityService : IIdentityService
{
    private const string InvalidErrorMessage = "Invalid credentials.";

    private readonly UserManager<User> _userManager;
    private readonly ITokenGenerator _tokenGenerator;

    public IdentityService(UserManager<User> userManager, ITokenGenerator tokenGenerator)
    {
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<Result<User>> Register(UserInputModel userInput)
    {
        if (userInput.Email == Constants.SystemUser)
            return Result<User>.Failure(new []{ $"{Constants.SystemUser} is preserved username for the system."});
        
        var user = new User
        {
            Email = userInput.Email,
            UserName = userInput.Email
        };

        var identityResult = await _userManager.CreateAsync(user, userInput.Password);

        return identityResult.Succeeded
            ? Result<User>.SuccessWith(user)
            : Result<User>.Failure(identityResult.Errors.Select(e => e.Description));
    }

    public async Task<Result<UserOutputModel>> Login(UserInputModel userInput)
    {
        var user = await _userManager.FindByEmailAsync(userInput.Email);
        if (user == null)
            return InvalidErrorMessage;

        var passwordValid = await _userManager.CheckPasswordAsync(user, userInput.Password);
        if (!passwordValid)
            return InvalidErrorMessage;

        var roles = await _userManager.GetRolesAsync(user);
        var isAdministrator = roles.Any(r => r == Constants.AdministratorRoleName);

        var token = _tokenGenerator.GenerateToken(user, roles);

        return new UserOutputModel(token, userInput.Email, isAdministrator);
    }

    public async Task<Result> ChangePassword(string userId, ChangePasswordInputModel changePasswordInput)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return InvalidErrorMessage;

        var identityResult = await _userManager.ChangePasswordAsync(
            user,
            changePasswordInput.CurrentPassword,
            changePasswordInput.NewPassword);

        return identityResult.Succeeded
            ? Result.Success
            : Result.Failure(identityResult.Errors.Select(e => e.Description));
    }
}