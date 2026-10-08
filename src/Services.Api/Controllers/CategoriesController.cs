using Microsoft.AspNetCore.Mvc;
using Services.Application.Categories.GetCategories;

namespace Services.Api.Controllers;

[ApiController]
[Route("api/v1/categories")]
public sealed class CategoriesController(ILogger<CategoriesController> logger, GetCategoriesUseCase getCategories) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        logger.LogInformation($"Request from IP: {ip}");
        foreach (var header in HttpContext.Request.Headers)
        {
            logger.LogInformation($"{header.Key}: {header.Value}");
        }
        return Ok(await getCategories.ExecuteAsync(cancellationToken));
    }

}
