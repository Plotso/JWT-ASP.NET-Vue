namespace JWTShowcase.Posts.Models;

using System.ComponentModel.DataAnnotations;
using static Data.DataConstants.Posts;
public class PublicPostInputModel
{
    [Required]
    [MinLength(MinContentLength)]
    [MaxLength(MaxContentLength)]
    public string Content { get; set; }
}