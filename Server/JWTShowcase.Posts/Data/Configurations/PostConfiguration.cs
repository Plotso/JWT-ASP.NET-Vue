namespace JWTShowcase.Posts.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

using static Data.DataConstants.Posts;

public class PostConfiguration  : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(MaxTitleLength);

        builder
            .Property(p => p.Content)
            .IsRequired()
            .HasMaxLength(MaxContentLength);
    }
}
