using Microsoft.EntityFrameworkCore;
using OkumaUygulamasi.API.Data;
using OkumaUygulamasi.API.Models;
using OkumaUygulamasi.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!db.Books.Any())
    {
        var sampleBook = new Book { Title = "Küçük Prens", Content = "Bütün büyükler bir zamanlar çocuktu...", UnlockCost = 0, IsLocked = false };
        db.Books.Add(sampleBook);
        db.SaveChanges();

        db.Questions.Add(new Question { BookId = sampleBook.Id, Text = "Küçük Prens'in çiçeði nedir?", CorrectAnswer = "Gül", Points = 10 });
        db.SaveChanges();
    }
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.MapBookEndpoints();
app.MapUserEndpoints();

app.Run();