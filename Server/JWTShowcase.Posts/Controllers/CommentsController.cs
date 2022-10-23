namespace JWTShowcase.Posts.Controllers;

using Data.Models;
using JWTShowcase.Controllers;
using JWTShowcase.Models;
using JWTShowcase.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

public class CommentsController : ApiController
{
    private const int UnexpectedErrorCode = -666;

    private readonly ICommentsService _comments;
    private readonly IPostsService _posts;
    private readonly ICurrentUserService _currentUser;
    private readonly IAuthorsService _authors;
    private readonly ILogger<CommentsController> _logger;

    public CommentsController(ICommentsService comments, ICurrentUserService currentUser, IAuthorsService authors, ILogger<CommentsController> logger, IPostsService posts)
    {
        _comments = comments;
        _currentUser = currentUser;
        _authors = authors;
        _logger = logger;
        _posts = posts;
    }

    [HttpGet]
    [Authorize]
    [Route(Id)]
    public async Task<ActionResult<CommentOutputModel>> Get(int id)
    {
        var comment = await _comments.Get(id);
        return comment != null ? comment : NotFound(Result.Failure("Comment does not exist."));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<int>> Create(CommentInputModel input)
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

            var post = await _posts.GetInternal(input.PostId);
            if (post == null)
                return BadRequest(Result.Failure($"No post found with {input.PostId}"));
        
            var comment = new Comment()
            {
                Content = input.Content,
                Author = author,
                Post = post
            };

            await _comments.Save(comment);

            return comment.Id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unexpected error during comment creation. UserID: {userId}", _currentUser.UserId);
            return UnexpectedErrorCode;
        }
    }

    [HttpPut]
    [Authorize]
    [Route(Id)]
    public async Task<ActionResult> Edit(int id, CommentInputModel input)
    {
        var comment = await _comments.GetDbComment(id);
        if (comment == null)
            return NotFound(Result.Failure($"No comment found with {id}"));
        
        var currentAuthor = await _authors.GetByUserId(_currentUser.UserId);
        if (!_currentUser.IsAdministrator && (currentAuthor == null || comment.Author.Username != currentAuthor.Username))
            return BadRequest(Result.Failure("Only admins and comments author can edit its content!"));
        
        comment.Content = input.Content;

        await _comments.Save(comment);

        return Result.Success;
    }

    [HttpDelete]
    [Authorize]
    [Route(Id)]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var comment = await _comments.Get(id);
        if (comment == null)
            return NotFound(Result.Failure($"No comment found with {id}"));
        
        var author = await _authors.GetByUserId(_currentUser.UserId);
        if (!_currentUser.IsAdministrator || author == null || comment.AuthorUsername != author.Username)
            return BadRequest(Result.Failure("Only admins and comments author can delete it!"));

        return await _comments.Delete(id);
    }
}