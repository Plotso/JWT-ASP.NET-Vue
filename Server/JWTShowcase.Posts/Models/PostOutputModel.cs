namespace JWTShowcase.Posts.Models;

using Data.Models;
using JWTShowcase.Models;

public record PostOutputModel : IMapFrom<Post>
{
    public int Id { get; init; }
    
    public string Title { get; init; }

    public string Content { get; init; }

    public string AuthorUsername { get; init; }
    
    public DateTime CreatedOn { get; init; }
    
    public DateTime ModifiedOn { get; init; }
    
    public IEnumerable<CommentOutputModel> Comments { get; init; }
}