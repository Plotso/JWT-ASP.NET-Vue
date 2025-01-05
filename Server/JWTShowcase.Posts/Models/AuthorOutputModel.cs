namespace JWTShowcase.Posts.Models;

using Data.Models;
using JWTShowcase.Models;

public record AuthorOutputModel : IMapFrom<Author>
{
    public int Id { get; init; }
    
    public string Username { get; init; }
    
    public IEnumerable<CommentOutputModel> Comments { get; init; }
    
    public IEnumerable<PostOutputModel> Posts { get; init; }
}