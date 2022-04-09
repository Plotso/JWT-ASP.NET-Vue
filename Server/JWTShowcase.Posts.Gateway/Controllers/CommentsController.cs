namespace JWTShowcase.Posts.Gateway.Controllers;

using JWTShowcase.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Posts;

public class CommentsController : ApiController
{
    private readonly ICommentsService _comments;

    public CommentsController(ICommentsService comments)
        => _comments = comments;

    [HttpGet]
    [Route(nameof(Get))]
    public async Task<CommentOutputModel> Get(int id) 
        => await _comments.Get(id);

    [HttpPost]
    [Authorize]
    [Route(nameof(Create))]
    public async Task<int> Create(CommentInputModel input) 
        => await _comments.Create(input);

    [HttpPut]
    [Authorize]
    [Route(nameof(Edit))]
    public async Task Edit(int id, CommentInputModel input) 
        => await _comments.Edit(id, input);

    [HttpDelete]
    [Authorize]
    [Route(nameof(Delete))]
    public async Task Delete(int id) 
        => await _comments.Delete(id);
}