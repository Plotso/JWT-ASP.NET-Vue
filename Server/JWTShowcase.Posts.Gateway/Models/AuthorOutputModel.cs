namespace JWTShowcase.Posts.Gateway.Models;

using Posts;

public record AuthorOutputModel(int Id, string Username, IEnumerable<CommentOutputModel> Comments, IEnumerable<PostOutputModel> Posts);