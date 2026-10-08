using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Services.Application;
using Services.Api.Controllers;
using Services.Infrastructure;
using Services.Infrastructure.Persistence.PostgreSQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

app.UseCors("AllowFrontend");

// using (var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

//     // Genera el SQL que EF Core utilizaría para crear el esquema
//     var sql = db.Database.GenerateCreateScript();

//     Console.WriteLine("========== CREATE SCRIPT ==========");
//     Console.WriteLine(sql);
//     Console.WriteLine("===================================");

//     // Crea la base/tablas si corresponde
//     db.Database.EnsureCreated();
// }
app.UseExceptionHandler();
if (app.Environment.IsDevelopment()) app.MapOpenApi();
app.MapControllers();
app.Run();

public sealed class GlobalExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var isValidation = exception is ArgumentException;
        httpContext.Response.StatusCode = isValidation ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = new ProblemDetails
            {
                Status = httpContext.Response.StatusCode,
                Title = isValidation ? "Invalid restaurant" : "An unexpected error occurred.",
                Detail = exception.Message
            }
        });
    }
}

public partial class Program { }
