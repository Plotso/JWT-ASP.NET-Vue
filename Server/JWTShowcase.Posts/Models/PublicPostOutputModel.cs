namespace JWTShowcase.Posts.Models;

using Data.Models;
using JWTShowcase.Models;

public record PublicPostOutputModel : IMapFrom<PublicPost>
{
    public int Id { get; init; }
    
    public string Content { get; init; }
    
    public string AuthorUsername { get; init; }
    
    public DateTime CreatedOn { get; init; }
    
    public DateTime ModifiedOn { get; init; }
}