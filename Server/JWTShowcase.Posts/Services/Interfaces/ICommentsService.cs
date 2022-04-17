namespace JWTShowcase.Posts.Services.Interfaces;

using Data.Models;
using JWTShowcase.Data;
using Models;

public interface ICommentsService : IDataService<Comment>
{
    Task<CommentOutputModel> Get(int id);

    Task<Comment> GetDbComment(int id);

    Task<bool> Delete(int id);

    Task<bool> DeletePermanently(int id);
}