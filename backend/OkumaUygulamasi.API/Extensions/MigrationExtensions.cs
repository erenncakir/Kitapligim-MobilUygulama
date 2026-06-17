using Microsoft.EntityFrameworkCore;
using OkumaUygulamasi.API.Data;
using OkumaUygulamasi.API.Models;

namespace OkumaUygulamasi.API.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();

            /*SeedData - Kütüphane boşsa örnek kitaplar ekle*/
            if (!dbContext.Books.Any())
            {
                var book1 = new Book
                {
                    Title = "Küçük Prens",
                    Content = "Bütün büyükler bir zamanlar çocuktu...",
                    UnlockCost = 0,
                    IsLocked = false
                };
                var book2 = new Book
                {
                    Title = "Kılıçustasının Macerası",
                    Content = "...kılıcını çekti ve ormanın derinliklerine doğru yola koyuldu...",
                    UnlockCost = 0,
                    IsLocked = false
                };
                var book3 = new Book
                {
                    Title = "Pinokyo",
                    Content = "Geppetto usta tahtadan bir kukla yaptı...",
                    UnlockCost = 20,
                    IsLocked = true
                };
                var book4 = new Book
                {
                    Title = "Gezegenler Arası Yolculuk",
                    Content = "Güneş sistemindeki en büyük gezegen Jüpiter'dir...",
                    UnlockCost = 40,
                    IsLocked = true
                };
                dbContext.Books.AddRange(book1, book2, book3, book4);
                dbContext.SaveChanges();

                var questions = new List<Question>
                {
                    new Question{
                        BookId = book1.Id,
                        Text = "Küçük Prens'in çiçeği nedir?",
                        Options = new List<string> { "Papatya", "Gül", "Lale", "Menekşe" },
                        CorrectAnswer = "Gül",
                        Points = 10},
                    new Question{
                        BookId = book1.Id,
                        Text = "Küçük Prens'in gezegeninde hangi ağaçlar tehlikededir?",
                        Options = new List<string> { "Çam", "Meşe", "Baobab", "Söğüt" },
                        CorrectAnswer = "Baobab",
                        Points = 10},

                    new Question{
                        BookId = book2.Id,
                        Text = "Kılıçustası ormanda hangi silahını çekti?",
                        Options = new List<string> { "Ok", "Kılıç", "Hançer", "Balta" },
                        CorrectAnswer = "Kılıç",
                        Points = 10},
                    new Question{
                        BookId = book2.Id,
                        Text = "Kılıçustası nereye doğru yürüdü?",
                        Options = new List<string> { "Dağın zirvesine", "Köy meydanına", "Ormanın derinliklerine", "Deniz kıyısına" },
                        CorrectAnswer = "Ormanın derinliklerine",
                        Points = 10},

                    new Question{
                        BookId = book3.Id,
                        Text = "Pinokyo yalan söyleyince neyi uzar?",
                        Options = new List<string> { "Kolları", "Kulakları", "Burnu", "Saçları" },
                        CorrectAnswer = "Burnu",
                        Points = 15},
                    new Question{
                        BookId = book3.Id,
                        Text = "Pinokyo'yu kim yapmıştır?",
                        Options = new List<string> { "Geppetto", "Gargamel", "Büyücü", "Kral" },
                        CorrectAnswer = "Geppetto",
                        Points = 15},

                    new Question{
                        BookId = book4.Id,
                        Text = "Güneş sistemizdeki en büyük gezegen hangisidir?",
                        Options = new List<string> { "Dünya", "Mars", "Satürn", "Jüpiter" },
                        CorrectAnswer = "Jüpiter",
                        Points = 20}

                };
                dbContext.Questions.AddRange(questions);
                dbContext.SaveChanges();
            }
        }
    }
}
