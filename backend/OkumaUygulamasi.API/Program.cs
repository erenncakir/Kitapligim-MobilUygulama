using OkumaUygulamasi.API.Data;
using OkumaUygulamasi.API.Endpoints;
using OkumaUygulamasi.API.Extensions;
using OkumaUygulamasi.API.Exceptions;

using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("General", opt =>
    {
        opt.Window = TimeSpan.FromSeconds(10);
        opt.PermitLimit = 30;
        opt.QueueLimit = 0;
    });

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;

        var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
        {
            Status = StatusCodes.Status429TooManyRequests,
            Title = "Çok Fazla Ýstek (Rate Limit)",
            Detail = "Fazla istek sebebiyle koruma amaçlý kýsa süreliðine engellendiniz. Lütfen birkaç saniye bekleyip tekrar deneyin."
        };

        await context.HttpContext.Response.WriteAsJsonAsync(problemDetails, token);
    };
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.ApplyMigrations(); /*SeedDatalar da burada*/

app.UseExceptionHandler();
app.UseRateLimiter();

/*GEÇÝCÝ OLARAK SWAGGER ORTAMA AÇILDI*/
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Okuma Uygulamasi API v1");
    options.RoutePrefix = string.Empty;
});
app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.MapBookEndpoints();
app.MapUserEndpoints();
app.MapQuestionEndpoints();

app.Run();