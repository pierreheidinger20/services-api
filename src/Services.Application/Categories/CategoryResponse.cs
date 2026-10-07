using System;

namespace Services.Application.Categories;

public sealed record CategoryResponse(
    Guid PublicId,
    string Name)
{
}
