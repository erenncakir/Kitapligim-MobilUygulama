namespace OkumaUygulamasi.API.Models
{
    public class User
    {

        /*
         * UUID - Universally Unique Identifier kullanarak verileri ayıracağız.
         * Flutterda benzersiz bir metin oluşturulacak.
        */

        public string Id { get; set; }
        public int TotalPoints { get; set; }
    }
}
