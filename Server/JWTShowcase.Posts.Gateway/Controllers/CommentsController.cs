namespace JWTShowcase.Posts.Gateway.Controllers;

using JWTShowcase.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Posts;

public class CommentsController : BaseGatewayController
{
    private readonly ICommentsService _comments;

    public CommentsController(ICommentsService comments, ILogger<CommentsController> logger)
        : base(logger)
        => _comments = comments;

    [HttpGet]
    [Route(nameof(Get))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentOutputModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int id) 
        => await SafeProcessRefitRequest(
            async () =>
            {
                var result = await _comments.Get(id);
                return Ok(result);
            }, $"An error occured Get comment by id for id: {id}"); 

    [HttpPost]
    [Authorize]
    [Route(nameof(Create))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(CommentInputModel input) 
        => await SafeProcessRefitRequest(
            async () =>
            {
                var postId = await _comments.Create(input);
                return Ok(postId);
            }, "An error occured during Creation of comment");

    [HttpPut]
    [Authorize]
    [Route(nameof(Edit))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Edit(int id, CommentInputModel input) 
        => await SafeProcessRefitRequest(
            async () =>
            {
                await _comments.Edit(id, input);
                return Ok();
            }, $"An error occured during edit attempt for comment with id: {id}"); 

    [HttpDelete]
    [Authorize]
    [Route(nameof(Delete))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id) 
        => await SafeProcessRefitRequest(
            async () =>
            {
                var isSuccess = await _comments.Delete(id);
                return Ok(isSuccess);
            }, $"An error occured during delete of comment with id: {id}."); 
}