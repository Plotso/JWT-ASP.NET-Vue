using JWTShowcase.Extensions;
using JWTShowcase.Posts.Data;
using JWTShowcase.Posts.Data.Models;
using JWTShowcase.Posts.Services;
using JWTShowcase.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddWebService<PostsDbContext>(builder.Configuration, swaggerEnabled: true)
    .AddTransient<IDataSeeder, PostDataSeeder>()
    .AddTransient<IPublicPostsService, PublicPostsService>()
    .AddTransient<IPostsService, PostsService>()
    .AddTransient<ICommentsService, CommentsService>()
    .AddTransient<IAuthorsService, AuthorsService>();

var app = builder.Build();
app
    .UseWebService(app.Environment, swaggerEnabled: true)
    .SeedData();

app.Run();