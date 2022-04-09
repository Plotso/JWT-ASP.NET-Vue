namespace JWTShowcase.Posts.Gateway.Models.Posts;

using JWTShowcase.Models;

public record PostsQuery(string Keyword, string Author, SortOptions SortOptions, int Page = 1, int PostPerPage = 15);