namespace OkumaUygulamasi.API.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Text { get; set; }
        public string CorrectAnswer { get; set; }
        public int Points { get; set; }
        public Book Book { get; set; }
    }
}
