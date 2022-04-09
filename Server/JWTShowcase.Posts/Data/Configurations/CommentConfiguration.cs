namespace JWTShowcase.Posts.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

using static Data.DataConstants.Comments;

public class CommentConfiguration  : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.Content)
            .IsRequired()
            .HasMaxLength(MaxContentLength);
    }
}
