namespace JWTShowcase.Posts.Services;

using AutoMapper;
using Data.Models;
using Interfaces;
using JWTShowcase.Data;
using Microsoft.EntityFrameworkCore;
using Models;

public class CommentsService : DataService<Comment>, ICommentsService
{
    private readonly IMapper _mapper;

    public CommentsService(DbContext db, IMapper mapper) : base(db)
    {
        _mapper = mapper;
    }

    public async Task<CommentOutputModel> Get(int id)
        => await _mapper
            .ProjectTo<CommentOutputModel>(All().Where(p => p.Id == id))
            .SingleOrDefaultAsync();

    public async Task<Comment> GetDbComment(int id)
        => await All().SingleOrDefaultAsync(p => p.Id == id);

    public async Task<bool> Delete(int id)
    {
        var comment = await Data.FindAsync<Comment>(id);
        if (comment == null)
            return false;

        comment.IsDeleted = true;
        comment.DeletedOn = DateTime.UtcNow;

        await Save(comment);
        return true;
    }

    public async Task<bool> DeletePermanently(int id)
    {
        var comment = await Data.FindAsync<Comment>(id);
        if (comment == null)
            return false;

        Data.Remove(comment);
        
        await Data.SaveChangesAsync();
        return true;
    }
}