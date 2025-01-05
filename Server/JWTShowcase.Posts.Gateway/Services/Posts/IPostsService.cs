namespace JWTShowcase.Posts.Gateway.Services.Posts;

using Models.Posts;
using Refit;

using static Constants;

public interface IPostsService
{
    [Get("/Posts/GetAll")]
    Task<PostsOutputModel> GetAll();
    
    [Get("/Posts/{id}")]
    Task<PostOutputModel> Get(int id);
    
    [Post("/Posts")]
    Task<int> Create(PostInputModel input);

    [Put("/Posts/{id}")]
    Task Edit(int id, PostInputModel input);

    [Delete("/Posts/{id}")]
    Task<bool> Delete(int id);
}