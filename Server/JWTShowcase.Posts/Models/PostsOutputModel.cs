namespace JWTShowcase.Posts.Models;

public record PostsOutputModel(IEnumerable<PostOutputModel> posts, int page, int totalPages, int totalCount);