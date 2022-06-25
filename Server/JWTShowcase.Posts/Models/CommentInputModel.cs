namespace JWTShowcase.Posts.Models;

using System.ComponentModel.DataAnnotations;
using static Data.DataConstants.Comments;

public record CommentInputModel
{
    [Required]
    [MinLength(MinContentLength)]
    [MaxLength(MaxContentLength)]
    public string Content { get; set; }
    
    public int PostId { get; set; }
}