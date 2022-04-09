namespace JWTShowcase.Posts.Gateway.Controllers;

using JWTShowcase.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.PublicPosts;
using Services.Posts;

public class PublicPostsController : ApiController
{
    private readonly IPublicPostsService _publicPosts;

    public PublicPostsController(IPublicPostsService publicPosts)
        => _publicPosts = publicPosts;

    [HttpGet]
    [Route(nameof(GetAll))]
    public async Task<PublicPostsOutputModel> GetAll() 
        => await _publicPosts.GetAll();

    [HttpPost]
    [Authorize]
    [Route(nameof(Add))]
    public async Task<int> Add(PublicPostInputModel input) 
        => await _publicPosts.Add(input);

    [HttpPut]
    [Authorize]
    [Route(nameof(Edit))]
    public async Task Edit(int id, PublicPostInputModel input) 
        => await _publicPosts.Edit(id, input);

    [HttpDelete]
    [Authorize]
    [Route(nameof(Delete))]
    public async Task Delete(int id) 
        => await _publicPosts.Delete(id);
}