namespace JWTShowcase.Posts.Gateway.Controllers;

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Models.PublicPosts;
using Refit;
using Services.Posts;

public class PublicPostsController : BaseGatewayController
{
    private readonly IPublicPostsService _publicPosts;

    public PublicPostsController(IPublicPostsService publicPosts, ILogger<PublicPostsController> logger) 
        : base(logger) 
        => _publicPosts = publicPosts;

    [HttpGet]
    [Route(nameof(GetAll))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublicPostsOutputModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll() 
        => await SafeProcessRefitRequest(
            async () =>
            {
                var publicPosts = await _publicPosts.GetAll();
                return Ok(publicPosts);
            }, "An error occured during public posts get request."); 

    [HttpPost]
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Route(nameof(Add))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add(PublicPostInputModel input) 
        => await SafeProcessRefitRequest(
            async () =>
                {
                    var postId = await _publicPosts.Add(input);
                    return Ok(postId);
                }, "An error occured during public post creation"); 

    [HttpPut]
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Route(nameof(Edit))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Edit(int id, PublicPostInputModel input) 
        => await SafeProcessRefitRequest(
            async () =>
            {
                await _publicPosts.Edit(id, input);
                return Ok();
            }, $"An error occured during edit of public post with id: {id}."); 

    [HttpDelete]
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Route(nameof(Delete))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id) 
        => await SafeProcessRefitRequest(
            async () =>
            {
                var isSuccess = await _publicPosts.Delete(id);
                return Ok(isSuccess);
            }, $"An error occured during delete of public post with id: {id}."); 
}