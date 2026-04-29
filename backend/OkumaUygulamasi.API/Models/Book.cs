namespace OkumaUygulamasi.API.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public int UnlockCost { get; set; }
        public bool IsLocked { get; set; }
    }
}
