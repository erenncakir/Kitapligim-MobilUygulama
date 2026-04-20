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

            bool hasOldBooks = dbContext.Books.Any(b => string.IsNullOrEmpty(b.Category));
            if (hasOldBooks)
            {
                dbContext.UserBooks.RemoveRange(dbContext.UserBooks);
                dbContext.Questions.RemoveRange(dbContext.Questions);
                dbContext.Books.RemoveRange(dbContext.Books);
                dbContext.SaveChanges();
            }
            string str = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce nec sem porta, faucibus ipsum vitae, ultrices magna. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Duis mi nibh, aliquam a tortor vel, posuere finibus urna. Nam facilisis commodo iaculis. Morbi finibus imperdiet tortor, et dictum urna egestas ac. Sed bibendum turpis lorem, ac pulvinar arcu pretium non. Suspendisse bibendum purus quam. Proin vulputate placerat eros ac laoreet. Integer mauris mi, congue at dapibus vitae, sodales in sem. Curabitur iaculis nisl eget risus congue, lacinia porta leo accumsan. Duis felis tellus, bibendum in egestas ac, pellentesque sed metus. Nulla facilisi.\r\n\r\nPraesent eget quam cursus, accumsan magna sed, accumsan felis. Sed convallis, tellus non egestas placerat, urna ante porta tellus, in cursus augue erat sit amet dolor. Quisque mattis sapien commodo est posuere, non viverra sem sodales. Nulla facilisi. Cras euismod nec ipsum vel tincidunt. Donec dapibus a tellus eget venenatis. Donec placerat augue diam, nec lobortis erat tempor nec. Praesent in ex nulla. Duis elementum quis libero et suscipit.\r\n\r\nAliquam id gravida quam. Integer faucibus ante vel lectus dictum porttitor. Nunc nec pharetra odio. Praesent placerat turpis eget massa iaculis scelerisque. Mauris laoreet fringilla ipsum et consequat. Aenean ornare ultricies risus vitae tristique. Cras ac pellentesque nulla, eu vulputate mauris. Vivamus ultricies purus eu magna molestie, convallis varius libero aliquam. Pellentesque eu sagittis neque, non consequat est. Mauris dignissim tincidunt tortor, in aliquam turpis aliquam et. Sed pharetra magna ut arcu imperdiet accumsan. Duis sed leo ut purus consectetur auctor eu vel purus. Duis tempor nisl tempus dui fermentum, ut consectetur lorem tincidunt. In mauris tortor, auctor in augue at, auctor viverra sem.\r\n\r\nInteger blandit augue eget metus pellentesque, quis condimentum nisl commodo. Suspendisse a gravida enim. Nullam eu nulla vitae est semper laoreet. Aenean convallis nisl ante, et ultrices nisi maximus sit amet. Quisque vehicula dui sit amet efficitur dignissim. Proin mattis dapibus scelerisque. Maecenas convallis nisi sed magna vehicula bibendum.\r\n\r\nDonec tempus diam dolor, at pulvinar felis hendrerit sed. Quisque erat massa, dignissim ut scelerisque quis, tristique placerat enim. Phasellus faucibus cursus gravida. Integer blandit nec nisl sit amet egestas. Etiam vitae fermentum nulla. Suspendisse aliquet ex et justo interdum, non dictum nisi fringilla. Praesent dictum quis nulla sed bibendum. Mauris at sapien sed lectus lacinia vestibulum. Nullam ullamcorper vulputate mauris sit amet placerat. Vestibulum aliquam dolor leo, eu rhoncus leo malesuada vitae. Proin felis quam, mattis non justo sit amet, posuere ornare nunc. Sed vel mi ultricies leo aliquet laoreet. Fusce ac accumsan odio. Proin dictum velit urna, a accumsan sapien tincidunt in. Aliquam malesuada tincidunt tellus, non rutrum libero volutpat ut.\r\n\r\nDuis gravida ornare felis, a pretium urna volutpat ac. Suspendisse et rutrum dui, a eleifend sem. Duis id magna sollicitudin, sodales lorem ut, accumsan risus. Nulla luctus magna dictum suscipit blandit. Duis iaculis quam eu velit dictum congue. Sed vitae ornare arcu. Donec dignissim diam felis, sit amet laoreet ipsum lobortis sed. Aenean erat turpis, interdum a mi ac, iaculis aliquet.";
            if (!dbContext.Books.Any())
            {
                var book1 = new Book
                {
                    Title = "Küçük Prens",
                    Category = "Klasikler",
                    Content = str,
                    UnlockCost = 0,
                    IsLocked = false
                };
                var book2 = new Book
                {
                    Title = "Kılıçustasının Macerası",
                    Category = "Macera",
                    Content = str,
                    UnlockCost = 0,
                    IsLocked = false
                };
                var book3 = new Book
                {
                    Title = "Pinokyo",
                    Category = "Masallar",
                    Content = str,
                    UnlockCost = 20,
                    IsLocked = true
                };
                var book4 = new Book
                {
                    Title = "Gezegenler Arası Yolculuk",
                    Category = "Bilim Kurgu",
                    Content =str,
                    UnlockCost = 40,
                    IsLocked = true
                };

                dbContext.Books.AddRange(book1, book2, book3, book4);
                dbContext.SaveChanges();

                var questions = new List<Question>
                {
                    // Küçük Prens Soruları
                    new Question { BookId = book1.Id, Text = "Küçük Prens'in çiçeği nedir?", Options = new List<string> { "Papatya", "Gül", "Lale", "Menekşe" }, CorrectAnswer = "Gül", Points = 10 },
                    new Question { BookId = book1.Id, Text = "Küçük Prens'in gezegeninde hangi ağaçlar tehlikededir?", Options = new List<string> { "Çam", "Meşe", "Baobab", "Söğüt" }, CorrectAnswer = "Baobab", Points = 10 },
                    
                    // Kılıçustası Soruları
                    new Question { BookId = book2.Id, Text = "Kılıçustası ormanda hangi silahını çekti?", Options = new List<string> { "Ok", "Kılıç", "Hançer", "Balta" }, CorrectAnswer = "Kılıç", Points = 10 },
                    new Question { BookId = book2.Id, Text = "Kılıçustası nereye doğru yürüdü?", Options = new List<string> { "Dağın zirvesine", "Köy meydanına", "Ormanın derinliklerine", "Deniz kıyısına" }, CorrectAnswer = "Ormanın derinliklerine", Points = 10 },

                    // Pinokyo Soruları
                    new Question { BookId = book3.Id, Text = "Pinokyo yalan söyleyince neyi uzar?", Options = new List<string> { "Kolları", "Kulakları", "Burnu", "Saçları" }, CorrectAnswer = "Burnu", Points = 15 },
                    new Question { BookId = book3.Id, Text = "Pinokyo'yu kim yapmıştır?", Options = new List<string> { "Geppetto", "Gargamel", "Büyücü", "Kral" }, CorrectAnswer = "Geppetto", Points = 15 },

                    // Bilim Kurgu Soruları
                    new Question { BookId = book4.Id, Text = "Güneş sistemizdeki en büyük gezegen hangisidir?", Options = new List<string> { "Dünya", "Mars", "Satürn", "Jüpiter" }, CorrectAnswer = "Jüpiter", Points = 20 }
                };

                dbContext.Questions.AddRange(questions);
                dbContext.SaveChanges();
            }
        }
    }
}