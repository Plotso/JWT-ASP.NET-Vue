namespace JWTShowcase.Posts.Gateway.Models.Posts;

using System.ComponentModel.DataAnnotations;

public class PostInputModel
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }
}