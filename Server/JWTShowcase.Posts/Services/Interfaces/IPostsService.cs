namespace JWTShowcase.Posts.Services.Interfaces;

using Data.Models;
using JWTShowcase.Data;
using Models;

public interface IPostsService : IDataService<Post>
{
    Task<IEnumerable<PostOutputModel>> GetAll();
    
    Task<IEnumerable<PostOutputModel>> Query(PostsQuery query);

    Task<int> Total(PostsQuery query);

    Task<PostOutputModel> Get(int id);

    Task<Post> GetDbPost(int id);

    Task<bool> Delete(int id);

    Task<bool> DeletePermanently(int id);

    Task<Post> GetInternal(int id);
}