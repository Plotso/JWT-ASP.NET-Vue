namespace JWTShowcase.Posts.Services;

using Data.Models;
using JWTShowcase.Data;
using Models;

public interface ICommentsService : IDataService<Comment>
{
    Task<CommentOutputModel> Get(int id);

    Task<bool> Delete(int id);

    Task<bool> DeletePermanently(int id);
}