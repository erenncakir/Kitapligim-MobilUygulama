using Microsoft.EntityFrameworkCore;
using OkumaUygulamasi.API.Data;
using OkumaUygulamasi.API.Models;
using OkumaUygulamasi.API.Extensions;

namespace OkumaUygulamasi.API.Endpoints
{
    public static class BookEndpoints
    {
        public static void MapBookEndpoints(this IEndpointRouteBuilder app)
        {
            var bookApi = app.MapVersionedGroup("books").RequireRateLimiting("General");

            bookApi.MapGet("/", async (AppDbContext db) =>
            {
                var books = await db.Books.ToListAsync();
                return Results.Ok(books);
            });
            bookApi.MapGet("/{id}", async (int id, AppDbContext db) =>
            {
                var book  = await db.Books.FindAsync(id);
                return book is not null ? Results.Ok(book) : Results.NotFound();
            });
        }
    }
}
