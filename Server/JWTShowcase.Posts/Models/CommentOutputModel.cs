namespace JWTShowcase.Posts.Models;

using Data.Models;
using JWTShowcase.Models;

public record CommentOutputModel(int Id, string Content, string AuthorUsername, DateTime CreatedOn, DateTime ModifiedOn) : IMapFrom<Comment>;