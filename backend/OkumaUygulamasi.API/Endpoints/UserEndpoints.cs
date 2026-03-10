using Microsoft.EntityFrameworkCore;
using OkumaUygulamasi.API.Data;
using OkumaUygulamasi.API.Models;

namespace OkumaUygulamasi.API.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var userApi = app.MapGroup("/api/v1/users");

            userApi.MapGet("/{deviceId}", async (string deviceId, AppDbContext db) =>
            {
                var user = await db.Users.FindAsync(deviceId);

                if (user == null)
                {
                    user = new User { Id = deviceId, TotalPoints = 0 };
                    db.Users.Add(user);
                    await db.SaveChangesAsync();
                }

                return Results.Ok(user);
            });

            userApi.MapPost("/earn-points", async (string userId, int points, AppDbContext db) =>
            {
                var user = await db.Users.FindAsync(userId);
                if (user == null) return Results.NotFound("Kullanıcı bulunamadı.");

                user.TotalPoints += points;
                await db.SaveChangesAsync();

                return Results.Ok(new { Message = "Puan eklendi", CurrentPoints = user.TotalPoints });
            });

            userApi.MapPost("/unlock-book", async (string userId, int bookId, AppDbContext db) =>
            {
                var user = await db.Users.FindAsync(userId);
                var book = await db.Books.FindAsync(bookId);

                if (user == null || book == null) return Results.NotFound("Kullanıcı veya kitap bulunamadı.");

                if (user.TotalPoints < book.UnlockCost)
                    return Results.BadRequest("Yetersiz puan!");

                var alreadyUnlocked = await db.UserBooks.AnyAsync(ub => ub.UserId == userId && ub.BookId == bookId);
                if (alreadyUnlocked) return Results.BadRequest("Kitap zaten açık.");

                user.TotalPoints -= book.UnlockCost;
                db.UserBooks.Add(new UserBook { UserId = userId, BookId = bookId });

                await db.SaveChangesAsync();

                return Results.Ok(new { Message = "Kitap kilidi açıldı!", RemainingPoints = user.TotalPoints });
            });
        }
    }
}
