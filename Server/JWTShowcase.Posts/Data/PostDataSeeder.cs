namespace JWTShowcase.Posts.Data;

using JWTShowcase.Services;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.Interfaces;

public class PostDataSeeder : IDataSeeder
{
    private readonly IPublicPostsService _publicPosts;
    private readonly IPostsService _postsService;
    private readonly ICommentsService _commentsService;
    private readonly IAuthorsService _authors;
    private readonly PostsDbContext _db;
    private readonly ILogger<PostDataSeeder> _logger;

    public PostDataSeeder(
        IPublicPostsService publicPosts,
        IPostsService postsService,
        ICommentsService commentsService,
        IAuthorsService authors,
        PostsDbContext db,
        ILogger<PostDataSeeder> logger)
    {
        _publicPosts = publicPosts;
        _postsService = postsService;
        _commentsService = commentsService;
        _authors = authors;
        _db = db;
        _logger = logger;
    }

    public void SeedData()
    {
        //It's recommended to have separate seeders per entity but since current solution is for demo purposes there is combined seeder for all 4 entities in Posts microservice

        Task
            .Run(async () =>
            {
                if (!_db.Authors.Any(a => a.Username == Constants.SystemUser))
                {
                    await _authors.Save(new Author
                    {
                        Username = Constants.SystemUser,
                        UserId = Guid.NewGuid().ToString(),
                        Comments = new List<Comment>(),
                        Posts = new List<Post>()
                    });
                    _logger.LogInformation("Author seeded");
                }

                var author = await _authors.GetSystemUser();

                if (!_db.PublicPosts.Any())
                {
                    await _publicPosts.Save(new PublicPost
                    {
                        Author = author,
                        Content =
                            "JWTShowcase is a simple project aiming towards showing how to integrate JWT token authentication in a solution that uses VueJS as client application and has ASP.NET Core server backend built with simplified microservices architecture. If you want to see post and comments our users have, don't hesitate to create an account and log in!"
                    });
                    _logger.LogInformation("Public post seeded");
                }

                if (!_db.Posts.Any())
                {
                    await _postsService.Save(new Post
                    {
                        Author = author,
                        Title = "Initial post seeded by our awesome backend solution",
                        Content =
                            "This post is the initial creation of our awesome backend solution. It represents the success of the whole project. There aren't enough words to describe how good it feels to see all the pieces come together and the whole project to work out. Join the discussion in the comments section!!!",
                        Comments = new List<Comment>()
                    });
                    _logger.LogInformation("Internal post seeded");
                }

                await Task.Delay(2000);
                var post = await _db.Posts.FirstAsync();
                if (!_db.Comments.Any())
                {
                    await _commentsService.Save(new Comment
                    {
                        Author = author,
                        Content = "This comments is the first member of all comments across the site. We're very happy to see you here in our minimal microservices solution. Please share you thoughts in the comments. We hope that you enjoy your time here! :)",
                        Post = post
                    });
                    _logger.LogInformation("Comment seeded");
                }
            })
            .GetAwaiter()
            .GetResult();
    }
}