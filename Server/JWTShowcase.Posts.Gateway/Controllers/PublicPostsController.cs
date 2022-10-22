namespace JWTShowcase.Posts.Gateway.Controllers;

using System.Net;
using JWTShowcase.Controllers;
using Microsoft.AspNetCore.Mvc;
using Models.PublicPosts;
using Refit;
using Services.Posts;

public class PublicPostsController : ApiController
{
    private readonly IPublicPostsService _publicPosts;
    private readonly ILogger<PublicPostsController> _logger;

    public PublicPostsController(IPublicPostsService publicPosts, ILogger<PublicPostsController> logger)
    {
        _publicPosts = publicPosts;
        _logger = logger;
    }

    [HttpGet]
    [Route(nameof(GetAll))]
    public async Task<PublicPostsOutputModel> GetAll() 
        => await _publicPosts.GetAll();

    [HttpPost]
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Route(nameof(Add))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add(PublicPostInputModel input)
    {
        try
        {
            var postId = await _publicPosts.Add(input);
            return Ok(postId);
        }
        catch (ApiException e)
        {
            _logger.LogError(e, $"An error occured during public post creation");
            return StatusCode((int) e.StatusCode);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"An error occured during public post creation");
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpPut]
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Route(nameof(Edit))]
    public async Task Edit(int id, PublicPostInputModel input) 
        => await _publicPosts.Edit(id, input);

    [HttpDelete]
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Route(nameof(Delete))]
    public async Task Delete(int id) 
        => await _publicPosts.Delete(id);
}