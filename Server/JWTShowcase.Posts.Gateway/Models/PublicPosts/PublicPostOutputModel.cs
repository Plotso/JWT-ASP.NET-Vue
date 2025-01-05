namespace JWTShowcase.Posts.Gateway.Models.PublicPosts;

public record PublicPostOutputModel(int Id, string Content, string AuthorUsername, DateTime CreatedOn, DateTime ModifiedOn);