namespace JWTShowcase.Posts.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

using static Data.DataConstants.Posts;

public class AuthorConfiguration  : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.Username)
            .IsRequired();

        builder
            .Property(p => p.Username)
            .IsRequired();
    }
}
