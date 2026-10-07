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
        return Ok(await getCategories.ExecuteAsync(cancellationToken));
    }

}
