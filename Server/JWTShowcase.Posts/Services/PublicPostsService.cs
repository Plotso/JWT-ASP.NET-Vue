namespace JWTShowcase.Posts.Services;

using AutoMapper;
using Data.Models;
using Interfaces;
using JWTShowcase.Data;
using Microsoft.EntityFrameworkCore;
using Models;

public class PublicPostsService : DataService<PublicPost>, IPublicPostsService
{
    private readonly IMapper _mapper;

    public PublicPostsService(DbContext db, IMapper mapper) : base(db)
    {
        _mapper = mapper;
    }

    public async Task<IEnumerable<PublicPostOutputModel>> GetAll()
        => await _mapper.ProjectTo<PublicPostOutputModel>(All()).ToListAsync();

    public async Task<PublicPostOutputModel> Get(int id) 
        => await _mapper
            .ProjectTo<PublicPostOutputModel>(All().Where(p => p.Id == id))
            .SingleOrDefaultAsync();

    public async Task<bool> Delete(int id)
    {
        var post = await Data.FindAsync<PublicPost>(id);
        if (post == null)
            return false;

        post.IsDeleted = true;
        post.DeletedOn = DateTime.UtcNow;

        await Save(post);
        return true;
    }

    public async Task<bool> DeletePermanently(int id)
    {
        var post = await Data.FindAsync<PublicPost>(id);
        if (post == null)
            return false;
        
        Data.Remove(post);
        
        await Data.SaveChangesAsync();
        return true;
    }
}