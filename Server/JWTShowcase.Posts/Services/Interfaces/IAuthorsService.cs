namespace JWTShowcase.Posts.Services.Interfaces;

using Data.Models;
using JWTShowcase.Data;

public interface IAuthorsService : IDataService<Author>
{
    Task<Author> GetByUserId(string userId);

    Task<Author> GetSystemUser();
}