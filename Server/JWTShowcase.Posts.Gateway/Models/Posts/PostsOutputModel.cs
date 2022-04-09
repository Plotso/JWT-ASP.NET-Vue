namespace JWTShowcase.Posts.Gateway.Models.Posts;

public record PostsOutputModel(IEnumerable<PostOutputModel> posts, int page, int totalPages, int totalCount);