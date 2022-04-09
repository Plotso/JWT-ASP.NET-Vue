namespace JWTShowcase.Posts.Gateway.Services.Posts;

using Models;
using Refit;

public interface ICommentsService
{
    [Get("/Comments/{id}")]
    Task<CommentOutputModel> Get(int id);
    
    [Post("/Comments")]
    Task<int> Create(CommentInputModel input);

    [Put("/Comments/{id}")]
    Task Edit(int id, CommentInputModel input);

    [Delete("/Comments/{id}")]
    Task<bool> Delete(int id);
}