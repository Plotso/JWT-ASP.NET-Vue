namespace JWTShowcase.Posts.Models;

public record PublicPostsOutputModel(IEnumerable<PublicPostOutputModel> posts);