namespace JWTShowcase.Posts.Gateway.Models.PublicPosts;

using System.ComponentModel.DataAnnotations;

public class PublicPostInputModel
{
    [Required]
    public string Content { get; set; }
}