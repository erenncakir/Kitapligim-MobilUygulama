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
                var book = await db.Books.FindAsync(id);
                return book is not null ? Results.Ok(book) : Results.NotFound();
            });

            bookApi.MapGet("/categories", async (AppDbContext db) =>
            {
                var categories = await db.Books
                    .Where(b => !string.IsNullOrEmpty(b.Category))
                    .Select(b => b.Category)
                    .Distinct()
                    .ToListAsync();

                return Results.Ok(categories);
            });

            bookApi.MapGet("/category/{categoryName}", async (string categoryName, AppDbContext db) =>
            {
                var books = await db.Books
                    .Where(b => b.Category.ToLower() == categoryName.ToLower())
                    .ToListAsync();

                return books.Any() ? Results.Ok(books) : Results.NotFound("Bu kategoride kitap bulunamadı.");
            });
        }
    }
}