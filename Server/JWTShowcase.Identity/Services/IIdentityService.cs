namespace JWTShowcase.Identity.Services;

using Data.Models;
using JWTShowcase.Models;
using Models;

public interface IIdentityService
{
    Task<Result<User>> Register(UserInputModel userInput);

    Task<Result<UserOutputModel>> Login(UserInputModel userInput);

    Task<Result> ChangePassword(string userId, ChangePasswordInputModel changePasswordInput);
}