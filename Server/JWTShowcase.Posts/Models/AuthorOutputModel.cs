namespace JWTShowcase.Posts.Models;

using Data.Models;
using JWTShowcase.Models;

public record AuthorOutputModel(int Id, string Username, IEnumerable<CommentOutputModel> Comments, IEnumerable<PostOutputModel> Posts) : IMapFrom<Author>;