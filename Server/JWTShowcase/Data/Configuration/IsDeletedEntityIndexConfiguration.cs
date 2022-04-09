namespace JWTShowcase.Data.Configuration;

using Microsoft.EntityFrameworkCore;
using Models.Interfaces;

public static class IsDeletedEntityIndexConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        // IDeletableEntity.IsDeleted index
        var deletableEntityTypes = modelBuilder.Model
            .GetEntityTypes()
            .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
        foreach (var deletableEntityType in deletableEntityTypes)
        {
            modelBuilder.Entity(deletableEntityType.ClrType).HasIndex(nameof(IDeletableEntity.IsDeleted));
        }
    }
}