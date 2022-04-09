namespace JWTShowcase.Posts.Gateway.Models;

using System.ComponentModel.DataAnnotations;

public class CommentInputModel
{
    [Required]
    public string Content { get; set; }
    
    public int PostId { get; set; }
}