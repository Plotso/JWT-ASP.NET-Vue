namespace JWTShowcase.Posts.Services;

using AutoMapper;
using Data;
using Data.Models;
using JWTShowcase.Data;
using Microsoft.EntityFrameworkCore;
using Models;

public class PostsService : DataService<Post>, IPostsService
{
    private readonly IMapper _mapper;
    private readonly ICommentsService _commentsService;

    public PostsService(PostsDbContext db, IMapper mapper, ICommentsService commentsService) : base(db)
    {
        _mapper = mapper;
        _commentsService = commentsService;
    }

    public async Task<IEnumerable<PostOutputModel>> GetAll() 
        => await _mapper.ProjectTo<PostOutputModel>(All()).ToListAsync();

    public async Task<IEnumerable<PostOutputModel>> Query(PostsQuery query)
        => (await _mapper
                .ProjectTo<PostOutputModel>(GetPostsQuery(query))
                .ToListAsync())
            .Skip((query.Page - 1) * query.PostPerPage)
            .Take(query.PostPerPage);

    public async Task<int> Total(PostsQuery query)
        => await GetPostsQuery(query).CountAsync();

    public async Task<PostOutputModel> Get(int id) 
        => await _mapper
            .ProjectTo<PostOutputModel>(All().Where(p => p.Id == id))
            .SingleOrDefaultAsync();

    public async Task<bool> Delete(int id)
    {
        var post = await Data.FindAsync<Post>(id);
        if (post == null)
            return false;

        if (post.Comments.Any())
        {
            foreach (var comment in post.Comments)
                await _commentsService.Delete(comment.Id);
        }

        post.IsDeleted = true;
        post.DeletedOn = DateTime.UtcNow;

        await Save(post);
        return true;
    }

    public async Task<bool> DeletePermanently(int id)
    {
        var post = await Data.FindAsync<Post>(id);
        if (post == null)
            return false;

        if (post.Comments.Any())
        {
            foreach (var comment in post.Comments)
                await _commentsService.DeletePermanently(comment.Id);
        }
        
        Data.Remove(post);
        
        await Data.SaveChangesAsync();
        return true;
    }

    public async Task<Post> GetInternal(int id)
        => await All()
            .Where(p => p.Id == id)
            .SingleOrDefaultAsync();

    private IQueryable<Post> GetPostsQuery(
        PostsQuery query, int? postId = null)
    {
        var dataQuery = this.All();

        if (postId.HasValue)
        {
            dataQuery = dataQuery.Where(c => c.Id == postId);
        }

        if (!string.IsNullOrWhiteSpace(query.Author))
        {
            dataQuery = dataQuery.Where(c => c.Author.Username == query.Author);
        }

        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            dataQuery = dataQuery.Where(c => c.Title.ToLowerInvariant().Contains(query.Keyword) || c.Content.ToLowerInvariant().Contains(query.Keyword));
        }

        dataQuery = SortQuery(dataQuery, query.SortOptions);

        return dataQuery;
    }

    private IQueryable<Post> SortQuery(IQueryable<Post> query, SortOptions options) => options switch
    {
        SortOptions.CreatedDescending => query.OrderByDescending(p => p.CreatedOn),
        SortOptions.UpdatedAscending => query.OrderBy(p => p.ModifiedOn),
        SortOptions.UpdatedDescending => query.OrderByDescending(p => p.ModifiedOn),
        _ => query // CreatedAscending, default ordering
    };
}