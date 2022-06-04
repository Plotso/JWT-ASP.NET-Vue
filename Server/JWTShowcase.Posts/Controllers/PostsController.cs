namespace JWTShowcase.Posts.Controllers;

using Data.Models;
using JWTShowcase.Controllers;
using JWTShowcase.Models;
using JWTShowcase.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

public class PostsController : ApiController
{
    private const int UnexpectedErrorCode = -666;
    
    private readonly IPostsService _posts;
    private readonly ICurrentUserService _currentUser;
    private readonly IAuthorsService _authors;
    private readonly ILogger<PostsController> _logger;

    public PostsController(
        IPostsService posts,
        ICurrentUserService currentUser,
        IAuthorsService authors,
        ILogger<PostsController> logger)
    {
        _posts = posts;
        _currentUser = currentUser;
        _authors = authors;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    [Route(nameof(GetAll))]
    public async Task<ActionResult<PostsOutputModel>> GetAll([FromQuery] PostsQuery query)
    {
        var posts = await _posts.Query(query);
        var totalItems = await _posts.Total(query);
        var totalPages = Math.Ceiling(totalItems / (decimal) query.PostPerPage);

        return new PostsOutputModel(posts, query.Page, (int)totalPages, totalItems);
    }

    [HttpGet]
    [Authorize]
    [Route(Id)]
    public async Task<ActionResult<PostOutputModel>> Get(int id)
    {
        var post = await _posts.Get(id);
        return post != null ? post : BadRequest(Result.Failure("Post does not exist."));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<int>> Create(PostInputModel input)
    {
        try
        {
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
        
            var post = new Post
            {
                Title = input.Title,
                Content = input.Content,
                Author = author,
                Comments = new List<Comment>()
            };

            await _posts.Save(post);

            return post.Id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unexpected error during post creation. UserID: {userId}", _currentUser.UserId);
            return UnexpectedErrorCode;
        }
    }

    [HttpPut]
    [Authorize]
    [Route(Id)]
    public async Task<ActionResult> Edit(int id, PostInputModel input)
    {
        var post = await _posts.GetDbPost(id);
        if (post == null)
            return BadRequest(Result.Failure($"No post found with {id}"));
        
        var author = await _authors.GetByUserId(_currentUser.UserId);
        if (!_currentUser.IsAdministrator && (author == null || post.Author.Username != author.Username))
            return BadRequest(Result.Failure("Only admins and post author can edit its content!"));
        
        post.Title = input.Title;
        post.Content = input.Content;

        await _posts.Save(post);

        return Result.Success;
    }

    [HttpDelete]
    [Authorize]
    [Route(Id)]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var post = await _posts.Get(id);
        if (post == null)
            return BadRequest(Result.Failure($"No post found with {id}"));
        
        var author = await _authors.GetByUserId(_currentUser.UserId);
        if (!_currentUser.IsAdministrator || author == null || post.AuthorUsername != author.Username)
            return BadRequest(Result.Failure("Only admins and post author can delete it!"));

        return await _posts.Delete(id);
    }
}