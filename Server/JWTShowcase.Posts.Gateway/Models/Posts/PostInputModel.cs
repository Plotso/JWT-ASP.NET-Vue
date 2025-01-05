namespace JWTShowcase.Posts.Gateway.Models.Posts;

using System.ComponentModel.DataAnnotations;

public record PostInputModel
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }
}