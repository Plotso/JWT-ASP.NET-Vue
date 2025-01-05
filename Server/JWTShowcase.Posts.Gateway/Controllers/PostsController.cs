namespace JWTShowcase.Posts.Gateway.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Posts;
using Services.Posts;

public class PostsController : BaseGatewayController
{
    private readonly IPostsService _posts;

    public PostsController(IPostsService posts, ILogger<PostsController> logger)
        : base(logger)
        => _posts = posts;

    [HttpGet]
    [Route(nameof(GetAll))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostsOutputModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll() 
        => await SafeProcessRefitRequest(
            async () =>
            {
                var result = await _posts.GetAll();
                return Ok(result);
            }, $"An error occured durring GetAll posts request."); 

    [HttpGet]
    [Route(nameof(Get))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostOutputModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int id) 
        => await SafeProcessRefitRequest(
            async () =>
            {
                var result = await _posts.Get(id);
                return Ok(result);
            }, $"An error occured Get post by id for id: {id}"); 

    [HttpPost]
    [Authorize]
    [Route(nameof(Create))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(PostInputModel input) 
        => await SafeProcessRefitRequest(
            async () =>
            {
                var postId = await _posts.Create(input);
                return Ok(postId);
            }, "An error occured during Creation of post"); 

    [HttpPut]
    [Authorize]
    [Route(nameof(Edit))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Edit(int id, PostInputModel input) 
        => await SafeProcessRefitRequest(
            async () =>
            {
                await _posts.Edit(id, input);
                return Ok();
            }, $"An error occured during edit attempt for post with id: {id}"); 

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
                var isSuccess = await _posts.Delete(id);
                return Ok(isSuccess);
            }, $"An error occured during delete of post with id: {id}."); 
}