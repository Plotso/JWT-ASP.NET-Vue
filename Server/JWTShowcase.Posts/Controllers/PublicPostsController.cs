namespace JWTShowcase.Posts.Controllers;

using Data.Models;
using JWTShowcase.Controllers;
using JWTShowcase.Models;
using JWTShowcase.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

public class PublicPostsController : ApiController
{
    private const int UnexpectedErrorCode = -666;
    private readonly IPublicPostsService _publicPosts;
    private readonly ICurrentUserService _currentUser;
    private readonly IAuthorsService _authors;
    private readonly ILogger<PostsController> _logger;

    public PublicPostsController(
        IPublicPostsService publicPosts,
        ICurrentUserService currentUser,
        IAuthorsService authors,
        ILogger<PostsController> logger)
    {
        _publicPosts = publicPosts;
        _currentUser = currentUser;
        _authors = authors;
        _logger = logger;
    }

    [HttpGet]
    public async Task<PublicPostsOutputModel> GetAll() => new(await _publicPosts.GetAll());

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<int>> Add(PublicPostInputModel input)
    {
        try
        {
            if (!_currentUser.IsAdministrator)
                return BadRequest("Only administrators are allowed to create public posts");
            
            var author = await _authors.GetByUserId(_currentUser.UserId);
            if (author == null)
            {
                await _authors.Save(new Author
                {
                    Username = _currentUser.Username,
                    UserId = _currentUser.UserId,
                    Comments = new List<Comment>(),
                    Posts = new List<Post>()
                });
                author = await _authors.GetByUserId(_currentUser.UserId);
            }
        
            var post = new PublicPost
            {
                Content = input.Content,
                Author = author
            };

            await _publicPosts.Save(post);

            return post.Id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unexpected error during public post creation. UserID: {userId}", _currentUser.UserId);
            return UnexpectedErrorCode;
        }
    }

    [HttpPut]
    [Authorize]
    [Route(Id)]
    public async Task<ActionResult> Edit(int id, PublicPostInputModel input)
    {
        var post = await _publicPosts.GetDbPublicPost(id);
        if (post == null)
            return BadRequest(Result.Failure($"No post found with {id}"));
        
        if (!_currentUser.IsAdministrator)
            return BadRequest(Result.Failure("Only admins can edit public posts content!"));
        
        post.Content = input.Content;

        await _publicPosts.Save(post);

        return Result.Success;
    }

    [HttpDelete]
    [Authorize]
    [Route(Id)]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var post = await _publicPosts.Get(id);
        if (post == null)
            return BadRequest(Result.Failure($"No post found with {id}"));
        
        if (!_currentUser.IsAdministrator)
            return BadRequest(Result.Failure("Only admins can delete public posts!"));

        return await _publicPosts.Delete(id);
    }
}