namespace JWTShowcase.Posts.Models;

using System.ComponentModel.DataAnnotations;
using static Data.DataConstants.Posts;

public class PostInputModel
{
    [Required]
    [MinLength(MinTitleLength)]
    [MaxLength(MaxTitleLength)]
    public string Title { get; set; }

    [Required]
    [MinLength(MinContentLength)]
    [MaxLength(MaxContentLength)]
    public string Content { get; set; }
}