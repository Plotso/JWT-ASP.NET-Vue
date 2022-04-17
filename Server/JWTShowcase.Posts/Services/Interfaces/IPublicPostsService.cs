namespace JWTShowcase.Posts.Services.Interfaces;

using Data.Models;
using JWTShowcase.Data;
using Models;

public interface IPublicPostsService : IDataService<PublicPost>
{
    Task<IEnumerable<PublicPostOutputModel>> GetAll();
    
    Task<PublicPostOutputModel> Get(int id);

    Task<PublicPost> GetDbPublicPost(int id);

    Task<bool> Delete(int id);

    Task<bool> DeletePermanently(int id);
}