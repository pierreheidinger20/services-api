using Microsoft.AspNetCore.Mvc;
using Services.Application.Categories.GetCategories;

namespace Services.Api.Controllers;

[ApiController]
[Route("api/v1/categories")]
public sealed class CategoriesController(GetCategoriesUseCase getCategories) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        Console.WriteLine($"Request from IP: {ip}");
        return Ok(await getCategories.ExecuteAsync(cancellationToken));
    }

}
