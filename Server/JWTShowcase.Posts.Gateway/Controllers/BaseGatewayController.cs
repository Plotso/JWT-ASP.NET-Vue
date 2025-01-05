namespace JWTShowcase.Posts.Gateway.Controllers;

using System.Net;
using System.Text.Json;
using JWTShowcase.Controllers;
using Microsoft.AspNetCore.Mvc;
using Refit;

public abstract class BaseGatewayController : ApiController
{
    private readonly ILogger _logger;

    protected BaseGatewayController(ILogger logger) 
        => _logger = logger;
    
    protected async Task<IActionResult> SafeProcessRefitRequest(Func<Task<IActionResult>> action, string errorMessage)
    {
        try
        {
            return await action();
        }
        catch (ApiException e)
        {
            _logger.LogError(e, $"ErrorMessage: {errorMessage}. Response content: {e.Content}");
            //ProcessErrors(e);
            return StatusCode((int) e.StatusCode, e.Content);
        }
        catch (Exception e)
        {
            _logger.LogError(e, errorMessage);
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }        
    
    private void ProcessErrors(ApiException exception)
    {
        if (exception.HasContent)
        {
            JsonSerializer
                .Deserialize<List<string>>(exception.Content)
                .ForEach(error => ModelState.AddModelError(string.Empty, error));
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Internal server error.");
        }
    }
}