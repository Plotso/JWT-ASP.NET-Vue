﻿namespace JWTShowcase.Posts.Data;

using System.Reflection;
using Configurations;
using JWTShowcase.Data.Configuration;
using JWTShowcase.Data.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

public class PostsDbContext : DbContext
{
    private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
        typeof(PostsDbContext).GetMethod(
            nameof(SetIsDeletedQueryFilter),
            BindingFlags.NonPublic | BindingFlags.Static);

    public PostsDbContext(DbContextOptions<PostsDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<PublicPost> PublicPosts { get; set; }
    
    public DbSet<Post> Posts { get; set; }
    
    public DbSet<Comment> Comments { get; set; }
    
    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        IsDeletedEntityIndexConfiguration.Configure(builder);
        EntityIndexesConfiguration.Configure(builder);

        var entityTypes = builder.Model.GetEntityTypes().ToList();

        // Set global query filter for not deleted entities only
        var deletableEntityTypes = entityTypes
            .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
        foreach (var deletableEntityType in deletableEntityTypes)
        {
            var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
            method.Invoke(null, new object[] { builder });
        }

        // Disable cascade delete
        var foreignKeys = entityTypes
            .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
        foreach (var foreignKey in foreignKeys)
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

    private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
        where T : class, IDeletableEntity
    {
        builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
    }

    private void ApplyAuditInfoRules()
    {
        var changedEntries = ChangeTracker
            .Entries()
            .Where(e =>
                e.Entity is IAuditInfo &&
                (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in changedEntries)
        {
            var entity = (IAuditInfo)entry.Entity;
            if (entry.State == EntityState.Added && entity.CreatedOn == default)
            {
                entity.CreatedOn = DateTime.UtcNow;
            }
            else
            {
                entity.ModifiedOn = DateTime.UtcNow;
            }
        }
    }
}
