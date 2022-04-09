namespace JWTShowcase.Posts.Gateway.Services.Posts;

using Models.PublicPosts;
using Refit;

public interface IPublicPostsService
{
    [Get("/PublicPosts")]
    Task<PublicPostsOutputModel> GetAll();
    
    [Post("/PublicPosts")]
    Task<int> Add(PublicPostInputModel input);

    [Put("/PublicPosts/{id}")]
    Task Edit(int id, PublicPostInputModel input);

    [Delete("/PublicPosts/{id}")]
    Task<bool> Delete(int id);
}