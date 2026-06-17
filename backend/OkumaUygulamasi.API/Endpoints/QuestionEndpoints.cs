using Microsoft.EntityFrameworkCore;
using OkumaUygulamasi.API.Data;
using OkumaUygulamasi.API.Extensions;

namespace OkumaUygulamasi.API.Endpoints
{
    public static class QuestionEndpoints
    {
        public static void MapQuestionEndpoints(this IEndpointRouteBuilder app)
        {
            var questionApi = app.MapVersionedGroup("questions").RequireRateLimiting("General");

            questionApi.MapGet("/book/{bookId}", async (int bookId, AppDbContext db) =>
            {
                var questions = await db.Questions
                .Where(q => q.BookId == bookId)
                .ToListAsync();

                return Results.Ok(questions);
            });

            questionApi.MapPost("/{id}/answer", async (int id, string userId, string userAnswer, AppDbContext db) =>
            {
                var question = await db.Questions.FindAsync(id);
                if (question == null) return Results.NotFound("Soru bulunamadı");

                var user = await db.Users.FindAsync(userId);
                if (user == null) return Results.NotFound("Kullanıcı bulunamadı");

                bool isCorrect = string.Equals(question.CorrectAnswer, userAnswer, StringComparison.OrdinalIgnoreCase);
                if (isCorrect)
                {
                    user.TotalPoints += question.Points;
                    await db.SaveChangesAsync();

                    return Results.Ok(new
                    {
                        IsCorrect = true,
                        Message = "Tebrikler, doğru cevap!",
                        EarnedPoints = question.Points,
                        TotalPoints = user.TotalPoints
                    });
                }
                else
                {
                    return Results.Ok(new
                    {
                        IsCorrect = false,
                        Message = "Maalesef yanlış cevap.",
                        EarnedPoints = 0,
                        TotalPoints = user.TotalPoints
                    });
                }
            });
        }
    }
}