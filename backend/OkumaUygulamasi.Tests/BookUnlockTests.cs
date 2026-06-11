using Microsoft.EntityFrameworkCore;
using OkumaUygulamasi.API.Data;
using OkumaUygulamasi.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkumaUygulamasi.Tests
{
    public class BookUnlockTests
    {
        [Fact]
        public async Task YeterliPuaniOlanKullanici_KitapKilidini_Acabilmeli()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);

            var user = new User { Id = "test-cihaz-1", TotalPoints = 50 };
            var book = new Book { Id = 3, Title = "Pinokyo", Content = "Test İçeriği" };

            context.Users.Add(user);
            context.Books.Add(book);
            await context.SaveChangesAsync();

            int requiredPoints = 20;

            bool isUnlocked = false;

            if (user.TotalPoints >= requiredPoints)
            {
                user.TotalPoints -= requiredPoints;

                var userBook = new UserBook { UserId = user.Id, BookId = book.Id };
                context.UserBooks.Add(userBook);

                await context.SaveChangesAsync();
                isUnlocked = true;
            }

            Assert.True(isUnlocked);
            Assert.Equal(30, user.TotalPoints);

            bool hasBook = await context.UserBooks.AnyAsync(ub => ub.UserId == "test-cihaz-1" && ub.BookId == 3);
            Assert.True(hasBook);
        }
    }
}
