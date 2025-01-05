namespace JWTShowcase.Posts.Gateway.Models;

public record CommentOutputModel(int Id, string Content, string AuthorUsername, DateTime CreatedOn, DateTime ModifiedOn);