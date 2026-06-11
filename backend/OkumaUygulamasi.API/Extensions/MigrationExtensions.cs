using Microsoft.EntityFrameworkCore;
using OkumaUygulamasi.API.Data;
using OkumaUygulamasi.API.Models;
using System.Xml.Linq;

namespace OkumaUygulamasi.API.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            dbContext.Database.Migrate();

        }
        private static void AddBookIfNotExists(AppDbContext dbContext, Book book, List<Question> questions)
        {
            if (!dbContext.Books.Any(b => b.Title == book.Title))
            {

                foreach (var question in questions)
                {
                    question.Book = book;
                }
                dbContext.Books.Add(book);
                dbContext.Questions.AddRange(questions);
                dbContext.SaveChanges();
            }
        }
    }
}