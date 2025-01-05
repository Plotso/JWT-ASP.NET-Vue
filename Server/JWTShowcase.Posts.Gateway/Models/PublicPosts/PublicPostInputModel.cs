namespace JWTShowcase.Posts.Gateway.Models.PublicPosts;

using System.ComponentModel.DataAnnotations;

public record PublicPostInputModel
{
    [Required]
    public string Content { get; set; }
}