namespace JWTShowcase.Posts.Data.Models
{
    using JWTShowcase.Data.Models.Interfaces;

    public class Comment : IDeletableEntity, IAuditInfo
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }
        
        public bool IsDeleted { get; set; }
    
        public DateTime? DeletedOn { get; set; }
    
        public DateTime CreatedOn { get; set; }
    
        public DateTime? ModifiedOn { get; set; }
    }
}
