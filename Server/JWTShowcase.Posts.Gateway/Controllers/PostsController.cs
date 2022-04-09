namespace JWTShowcase.Posts.Gateway.Controllers;

using JWTShowcase.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Posts;
using Services.Posts;

public class PostsController : ApiController
{
    private readonly IPostsService _posts;

    public PostsController(IPostsService posts)
        => _posts = posts;

    [HttpGet]
    [Route(nameof(GetAll))]
    public async Task<PostsOutputModel> GetAll() 
        => await _posts.GetAll();

    [HttpGet]
    [Route(nameof(Get))]
    public async Task<PostOutputModel> Get(int id) 
        => await _posts.Get(id);

    [HttpPost]
    [Authorize]
    [Route(nameof(Create))]
    public async Task<int> Create(PostInputModel input) 
        => await _posts.Create(input);

    [HttpPut]
    [Authorize]
    [Route(nameof(Edit))]
    public async Task Edit(int id, PostInputModel input) 
        => await _posts.Edit(id, input);

    [HttpDelete]
    [Authorize]
    [Route(nameof(Delete))]
    public async Task Delete(int id) 
        => await _posts.Delete(id);
}