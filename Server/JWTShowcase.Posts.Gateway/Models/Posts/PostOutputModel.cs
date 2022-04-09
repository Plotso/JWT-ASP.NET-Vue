namespace JWTShowcase.Posts.Gateway.Models.Posts;

public record PostOutputModel(int Id, string Title, string Content, string AuthorUsername, IEnumerable<CommentOutputModel> Comments);