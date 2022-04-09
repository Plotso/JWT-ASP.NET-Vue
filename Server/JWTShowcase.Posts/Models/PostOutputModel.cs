namespace JWTShowcase.Posts.Models;

using Data.Models;
using JWTShowcase.Models;

public record PostOutputModel(int Id, string Title, string Content, string AuthorUsername, IEnumerable<CommentOutputModel> Comments) : IMapFrom<Post>;