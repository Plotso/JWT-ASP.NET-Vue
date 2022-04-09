namespace JWTShowcase.Posts.Gateway.Models.PublicPosts;

public record PublicPostsOutputModel(IEnumerable<PublicPostOutputModel> posts);