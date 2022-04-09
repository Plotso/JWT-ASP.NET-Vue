namespace JWTShowcase.Posts.Models;

public record PostsQuery(string Keyword, string Author, SortOptions SortOptions, int Page = 1, int PostPerPage = 15);

public enum SortOptions
{
    CreatedDescending,
    CreatedAscending,
    UpdatedAscending,
    UpdatedDescending
}