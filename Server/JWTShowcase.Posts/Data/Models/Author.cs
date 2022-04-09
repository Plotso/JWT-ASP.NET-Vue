namespace JWTShowcase.Posts.Data.Models
{
    using JWTShowcase.Data.Models.Interfaces;

    public class Author : IDeletableEntity
    {
        public int Id { get; set; }
        
        public string Username { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public string UserId { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public DateTime? DeletedOn { get; set; }
    }
}
