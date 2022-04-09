namespace JWTShowcase.Posts.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

using static Data.DataConstants.Posts;

public class PublicPostConfiguration  : IEntityTypeConfiguration<PublicPost>
{
    public void Configure(EntityTypeBuilder<PublicPost> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.Content)
            .IsRequired()
            .HasMaxLength(MaxContentLength);
    }
}
