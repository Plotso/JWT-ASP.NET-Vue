namespace JWTShowcase.Posts.Services;

using Data.Models;
using JWTShowcase.Data;
using Models;

public interface IAuthorsService : IDataService<Author>
{
    Task<Author> GetByUserId(string userId);

    Task<Author> GetSystemUser();
}