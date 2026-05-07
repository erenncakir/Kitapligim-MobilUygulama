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

            #region Kitap Ekleme

            AddBookIfNotExists(dbContext,
            new Book
            {
                Title = "Parlayan Yıldızlar, Evrenin Işıltılı Fenerleri",
                Category = "Uzayın Gizemleri",
                Description = @"Şehrin ışıklarından çok uzakta, sessiz bir orman kampında minik Emre'nin gökyüzündeki pırıl pırıl yıldızların sırlarını keşfetme macerasına katılmaya ne dersiniz? Bu sıcacık hikâyede çocuklar; yıldızların aslında devasa ateş topları olduğunu, en sıcak yıldızların hangi renkte parladığını ve gökyüzündeki şekil bulmaca oyunu olan takımyıldızlarını eğlenerek öğrenecekler.",
                Content = @"Şehrin gürültüsünden, kalabalığından ve parlak sokak lambalarından çok uzakta, çam ağaçlarıyla dolu bir ormandaydılar. Yedi yaşındaki Emre, ailesiyle birlikte hafta sonu kampına gelmişti. Etrafta sadece cırcır böceklerinin tatlı şarkısı ve hafifçe esen rüzgârın yapraklarda çıkardığı hışırtı vardı.

Hava tamamen karardığında, Emre çadırından dışarı adım attı. Başını gökyüzüne doğru kaldırdı ve olduğu yerde donup kaldı. Gözlerine inanamıyordu! Gökyüzü, simsiyah kocaman bir kadife örtüye benziyordu ve üzeri binlerce, hatta milyonlarca parlak, gümüş rengi noktayla kaplıydı.

Emre, yanına gelip kamp sandalyesine oturan babasına heyecanla fısıldadı: ""Baba, baksana! Gökyüzüne milyonlarca minik ateşböceği yapışmış gibi! Çok güzeller...""

Babası Emre’nin bu şaşkın ve sevimli haline gülümsedi. ""Onlar ateşböceği değil Emre'ciğim, onlar evrenin ışıltılı fenerleri, yani yıldızlar.""

**Sence gökyüzündeki bu parlayan noktalar, gerçekten bir ateşböceği veya minik birer el feneri kadar küçük olabilir mi?**

Aslında hiç de küçük değiller! Emre’nin babası anlatmaya başladı: ""Biliyor musun, o küçücük gördüğün yıldızların her biri, dünyamızdan bile milyonlarca kat büyük olan kocaman gaz ve ateş toplarıdır! O kadar büyük ve parlaktırlar ki, çok ama çok uzaklarda, uzayın derinliklerinde olmalarına rağmen ışıkları karanlığı aşıp gözümüze kadar ulaşır. Sadece bizden çok uzakta oldukları için bize minicik görünürler.""

Emre gözlerini kırpıştırdı. Uzaktaki dev ateş topları... Bu gerçekten büyüleyiciydi.

""Peki,"" dedi Emre düşünerek. ""Madem yıldızlar kocaman ateş topları... Neden bizi hiç ısıtmıyorlar? Neden sadece gece çıkıyorlar?""

**İşte burada kocaman bir sır var! Peki, sence gökyüzündeki en büyük, gündüzleri her yeri aydınlatan ve bizi sıcacık ısıtan o sarı top aslında nedir?**

Babası Emre’nin burnuna hafifçe dokunup güldü. ""Gündüzleri gökyüzünde parlayan Güneş var ya? İşte Güneş de aslında bir yıldızdır! Hem de bize en yakın olan yıldız. Bize o kadar yakındır ki, onun sıcaklığını tenimizde hissederiz ve gündüz olduğunda onun güçlü ışığı yüzünden diğer uzak yıldızları göremeyiz. Diğer yıldızlar gece çıkmazlar, onlar hep oradadır. Sadece Güneş saklanıp hava karardığında kendilerini gösterirler.""

Emre Güneş'in de bir yıldız olduğunu öğrenince çok şaşırdı. Gökyüzüne daha dikkatli bakmaya başladı. Bütün yıldızların aynı renkte olmadığını fark etti. Çoğu beyaz veya sarı gibiydi ama bazıları kırmızımsı, bazıları ise mavi mavi parlıyordu.

**Normalde evimizdeki ocağı yaktığımızda mavi ateş mi daha sıcaktır, yoksa kırmızı ateş mi? Sence mavi renkli bir yıldız mı daha sıcaktır, yoksa kırmızı renkli bir yıldız mı?**

""Bak şu mavi parlayan yıldıza,"" dedi babası parmağıyla göstererek. ""Yıldızların rengi, onların ne kadar sıcak olduğunu gösterir. Tıpkı ocaktaki ateşin en sıcak kısmının mavi olması gibi, uzaydaki mavi yıldızlar da en sıcak yıldızlardır! Kırmızı yıldızlar ise mavi olanlara göre daha serindir.""

Emre mavi yıldızlara bakarken, gökyüzünün sanki kocaman bir bulmaca tahtası olduğunu düşündü. Yıldızları hayali çizgilerle birbirine birleştirmeye çalıştı.

Babası, ""Gökyüzündeki o noktaları birleştirme oyununu çok eski zamanlardaki insanlar da oynamış,"" dedi. ""Yıldızları birbirine bağlayıp çeşitli hayvanlara, kahramanlara ve eşyalara benzetmişler. Bunlara 'Takımyıldızı' diyoruz. Mesela şuraya bak... Oradaki yedi yıldızı birleştirince sapı olan kocaman bir cezveye veya kepçeye benziyor, gördün mü? İşte onun adı Büyük Ayı Takımyıldızı.""

Emre parmağıyla babasının gösterdiği kepçe şeklindeki yıldızları havada çizdi. Gerçekten de oradaydı! Artık gökyüzü sadece noktalardan ibaret değildi, gizli resimlerle doluydu.

Tam o sırada, gökyüzünün bir ucundan diğer ucuna ışıldayan incecik bir çizgi çekildi. _Vıııızz!_ Çok hızlı, parlak bir şey kayıp gözden kayboldu.

""Aaa! Baba, yıldız düştü! Bir yıldız yerinden koptu!"" diye bağırdı Emre telaşla.

**Sence devasa bir ateş topu olan yıldızlar, yerlerinden kopup dünyamıza düşebilir mi?**

""Korkma kâşif Emre,"" diye gülümsedi babası. ""Ona 'kayan yıldız' deriz ama aslında o düşen bir yıldız değildir. Uzayda başıboş dolaşan minik kaya parçaları, yani göktaşları, çok hızlı bir şekilde dünyamızın havasına girdiklerinde sürtünmeden dolayı yanarlar ve arkalarında böyle parlak bir ışık bırakırlar. Sanki bir yıldız kayıyormuş gibi görünür.""

Emre derin bir oh çekti. Evrenin sırları ne kadar da eğlenceli ve şaşırtıcıydı. Güneş bir yıldızdı, takımyıldızları gökyüzü bulmacalarıydı, mavi yıldızlar çok sıcaktı ve kayan yıldızlar aslında minik uzay taşlarıydı!

Uykusu iyice gelen Emre, son bir kez o devasa ışıklı ormana, milyonlarca yıldıza el salladı. Çadırına girip tulumunun içine kıvrıldığında, kendini o yıldızların arasında, uzayın huzurlu kucağında hissetti. Belki de rüyasında Büyük Ayı ile saklambaç oynayacaktı.

##### Bugün Ne Öğrendik?

- **Dev Ateş Topları:** Yıldızlar uzakta oldukları için minicik görünseler de, aslında dünyamızdan milyonlarca kat büyük olan devasa gaz ve ateş toplarıdır.
    
- **En Yakın Yıldız:** Gündüzleri bizi ısıtan ve aydınlatan Güneş, aslında dünyamıza en yakın olan yıldızdır.
    
- **Renkler ve Sıcaklık:** Yıldızların rengi sıcaklıklarını gösterir. Mavi renkte parlayan yıldızlar, kırmızı renkli yıldızlardan çok daha sıcaktır.
    
- **Gökyüzü Bulmacaları (Takımyıldızları):** İnsanların yıldızları hayali çizgilerle birleştirerek hayvanlara veya nesnelere benzettiği şekillere takımyıldızı denir (Örneğin; Büyük Ayı).
    
- **Kayan Yıldızlar:** Gökyüzünde hızla kayıp giden parlak çizgiler aslında yerinden kopan yıldızlar değil, atmosferimize girip yanan göktaşlarıdır.",
                UnlockCost = 0,
                IsLocked = false
            },
            new List<Question>
            {
                new Question{
                    Text = "Hikâyemize göre, gündüzleri havayı aydınlattığı için diğer yıldızları görmemizi engelleyen, bize en yakın yıldızın adı nedir?",
                    Options = new List<string>
                    {
                        "Büyük Ayı",
                        "Mars",
                        "Güneş",
                        "Ay"
                    },
                    CorrectAnswer = "Güneş",
                    Points = 25
                },
                new Question{
                    Text = "Yıldızların renkleri onların sıcaklığını belli eder. En sıcak olan yıldızlar hangi renkte parlar?",
                    Options = new List<string>
                    {
                        "Mavi",
                        "Kırmızı",
                        "Yeşil",
                        "Pembe"
                    },
                    CorrectAnswer = "Mavi",
                    Points = 25
                },
                new Question{
                    Text = "Gökyüzündeki yıldızları hayali çizgilerle birleştirerek oluşturulan, kepçeye veya çeşitli hayvanlara benzeyen gruplara ne ad verilir?",
                    Options = new List<string>
                    {
                        "Uzay Çöpleri",
                        "Takımyıldızı",
                        "Gezegen Halkaları",
                        "Kara Delik"
                    },
                    CorrectAnswer = "Takımyıldızı",
                    Points = 25
                },
                new Question{
                    Text = "Kayan yıldız olarak adlandırdığımız, gökyüzünde aniden parlayıp sönen hızlı çizgiler aslında nedir?",
                    Options = new List<string>
                    {
                        "Uzaylıların fenerleri",
                        "Dünyaya yaklaşan bulutlar",
                        "Gerçekten yerinden kopan yıldızlar",
                        "Dünyanın havasına girip yanan minik göktaşları"
                    },
                    CorrectAnswer = "Dünyanın havasına girip yanan minik göktaşları",
                    Points = 25
                }
            }
            );
            AddBookIfNotExists(dbContext,
            new Book
            {
                Title = "Gümüş Ay, Gecenin Sihirli Lambası",
                Category = "Uzayın Gizemleri",
                Description = @"Gece gökyüzünü izlemeyi çok seven minik Duru'nun, kırmızı teleskopuyla Ay'a doğru yaptığı büyüleyici yolculuğa hazır mısınız? Bu sıcacık hikâyede çocuklar; gecemizi aydınlatan gümüş Ay'ın aslında kendi ışığı olmadığını, üzerindeki gizemli çukurların sırrını ve Ay'daki ayak izlerinin neden asla silinmediğini eğlenerek öğrenecekler.",
                Content = @"Gece olmuş, güneş çoktan uyumaya gitmişti. Gökyüzü koyu mavi, yıldızlı bir battaniye gibi şehrin üzerini örtmüştü. Yedi yaşındaki Duru, yatağına uzanmış, pencereden gökyüzünü izliyordu. Odasının ışığı kapalıydı ama içerisi hiç de karanlık değildi. Pencereden içeri giren gümüş rengi, pırıl pırıl bir ışık tüm odayı aydınlatıyordu.

Duru yataktan fırladı ve pencereye koştu. Gökyüzünde kocaman, yuvarlak ve parlak bir top duruyordu: Ay!

Kendi kendine mırıldandı: ""Acaba Ay'ın içinde kocaman bir ampul mü var? Gökyüzünde asılı duran bu top nasıl bu kadar parlak olabiliyor?""

**Sence uzayın o karanlık boşluğunda, pille veya elektrikle çalışan dev bir lamba olabilir mi?**

Elbette olamaz! Duru, babasının ona sürpriz olarak aldığı kırmızı teleskopunu hemen pencerenin önüne kurdu. Bir gözünü kapatıp diğerini merceğe dayadı ve Ay'a yakından bakmaya başladı. Biliyor musun, Ay aslında dev ve sihirli bir ayna gibidir! Ay'ın kendi ışığı yoktur. Gündüzleri bizi ısıtan Güneş var ya? İşte o Güneş, gece olduğunda ışıklarını uzaaaaklardan Ay'a gönderir. Ay da o ışıkları yakalayıp dünyamıza, Duru'nun odasına kadar yansıtır. Yani Ay, Güneş'in gece nöbetçisidir!

Duru teleskopuyla bakarken bir şey daha fark etti. Ay bugün kocaman bir tepsi gibi yuvarlak ve dolgundu. Ama geçen hafta gökyüzüne baktığında yarım bir elmaya benziyordu. Daha önceki haftalarda ise incecik, gümüş bir muz gibiydi!

**Sence Ay neden sürekli şekil değiştiriyor? Acaba gökyüzünde gizli bir uzaylı onu her gece biraz biraz ısırarak yiyor olabilir mi?**

Duru bu fikre kıkırdayarak güldü. Aslında kimse Ay'ı yemiyordu. Ay, dünyamızın etrafında durmadan döner. Tıpkı etrafımızda dönerek saklambaç oynayan bir arkadaşımız gibi! O dünyamızın etrafında dönerken, Güneş'in ışığı onun bazen sadece bir kısmını aydınlatır. Biz de dünyadan baktığımızda Ay'ı bazen incecik bir hilal, bazen yarım, bazen de kocaman yuvarlak bir ""Dolunay"" şeklinde görürüz.

Duru teleskopun ayarını minik parmaklarıyla biraz daha netleştirdi. ""Aaa!"" dedi şaşkınlıkla. Ay'a yakından bakınca yüzeyinin hiç de pürüzsüz olmadığını gördü. Üzerinde kocaman, gri lekeler ve derin çukurlar vardı. ""Sanki delikli, büyük bir peynire benziyor!"" diye düşündü.

O kocaman çukurlara ""krater"" deniyordu. Uzayda başıboş dolaşan serseri taşlar, yani göktaşları, milyonlarca yıl boyunca Ay'a çarpmıştı. Çarptıkları yerlerde böyle devasa çukurlar açmışlardı. Duru, o çukurların içinde gizlenen uzay gemileri hayal etti ama orada kimsecikler yoktu. Çok sessiz ve huzurlu bir yerdi.

Sonra Duru'nun aklına çok ilginç bir bilgi geldi. Çok uzun zaman önce, cesur astronotlar alevler saçan dev roketlerine binip dünyadan ayrılmış ve Ay'a gitmişlerdi. Hatta Ay'ın yüzeyinde yürüyüp o gri toprağa ayak izlerini bırakmışlardı.

**Peki sence, kumsalda yürüdüğümüzde bıraktığımız ayak izlerini dalgaların ve rüzgârın silmesi gibi, astronotların Ay'daki ayak izleri de uçup gitmiş midir?**

İşte uzayın en sihirli sırlarından biri daha! Ay'da hava yoktur. Rüzgâr esmez, yağmur yağmaz, fırtına kopmaz. Bu yüzden o cesur astronotların bıraktığı ayak izleri, hiçbir zaman silinmez! Milyonlarca yıl geçse bile tıpkı ilk günkü gibi orada dururlar. Duru teleskopuyla o minicik ayak izlerini göremese de, onların yukarıda bir yerlerde hâlâ durduğunu bilmek içini ısıttı.

Duru kocaman esnedi. Göz kapakları ağırlaşmaya başlamıştı. Ay, tüm sessizliği ve parlaklığıyla yukarıdan ona gülümsüyor gibiydi. O, dünyamızın en yakın arkadaşı, karanlık gecelerin en güzel lambasıydı.

Duru kırmızı teleskopunu usulca kapattı. Yatağına girdi, yumuşacık yorganını çenesine kadar çekti. Gümüş Ay'ın tatlı ışığı onu sarıp sarmalarken, rüyasında Ay'ın üzerinde astronot kıyafetiyle hoplaya zıplaya yürüdüğünü göreceğini biliyordu.

##### Bugün Ne Öğrendik?

- **Ayna Görevi:** Ay'ın kendi ışığı yoktur. Tıpkı dev bir ayna gibi, Güneş'ten aldığı ışığı yansıtarak gecelerimizi aydınlatır.
    
- **Ay'ın Şekilleri (Evreleri):** Ay, dünyamızın etrafında döndüğü için Güneş ışığını her zaman aynı açıdan almaz. Bu yüzden onu bazen hilal (muz şekli), bazen de dolunay (tam yuvarlak) olarak görürüz.
    
- **Peynir Gibi Çukurlar:** Ay'ın yüzeyi pürüzsüz değildir. Uzaydaki göktaşlarının çarpması sonucu oluşan ve ""Krater"" adı verilen dev çukurlarla doludur.
    
- **Silinmeyen İzler:** Ay'da hava, rüzgâr veya yağmur yoktur. Bu nedenle Ay'a giden astronotların toprakta bıraktığı ayak izleri asla bozulmaz ve sonsuza kadar kalır.",
                UnlockCost = 50,
                IsLocked = true
            },
            new List<Question>
            {
                new Question{
                    Text = "Hikâyemize göre Ay gece gökyüzünde nasıl bu kadar parlak görünebiliyor?",
                    Options = new List<string>
                    {
                        "İçinde dev bir ampul olduğu için",
                        "Güneş'ten aldığı ışığı ayna gibi yansıttığı için",
                        "Üzerinde ateş yandığı için",
                        "Ateşböcekleri Ay'ı kapladığı için"
                    },
                    CorrectAnswer = "Güneş'ten aldığı ışığı ayna gibi yansıttığı için",
                    Points = 25
                },
                new Question{
                    Text = "Ay'ın şeklinin bazen incecik bir muz (hilal), bazen de kocaman bir tepsi (dolunay) gibi görünmesinin sebebi nedir?",
                    Options = new List<string>
                    {
                        "Uzaylıların onu ısırması",
                        "Ay'ın rüzgârda küçülüp büyümesi",
                        "Ay'ın dünyamızın etrafında dönmesi ve Güneş ışığının farklı yerlerine vurması",
                        "Bulutların onu sıkıştırması"
                    },
                    CorrectAnswer = "Ay'ın dünyamızın etrafında dönmesi ve Güneş ışığının farklı yerlerine vurması",
                    Points = 25
                },
                new Question{
                    Text = "Ay'ın yüzeyinde bulunan, göktaşlarının çarpmasıyla oluşmuş derin çukurlara ne ad verilir?",
                    Options = new List<string>
                    {
                        "Krater",
                        "Kanyon",
                        "Havuz",
                        "Mağara"
                    },
                    CorrectAnswer = "Krater",
                    Points = 25
                },
                new Question{
                    Text = "Cesur astronotların Ay yüzeyinde bıraktıkları ayak izleri neden aradan çok uzun yıllar geçse de silinmez?",
                    Options = new List<string>
                    {
                        "Ayak izlerinin üzerini camla kapattıkları için",
                        "Ay'da rüzgâr, yağmur ve hava olmadığı için",
                        "Uzaylılar her gün ayak izlerini temizlediği için",
                        "Ayak izlerini boyadıkları için"
                    },
                    CorrectAnswer = "Ay'da rüzgâr, yağmur ve hava olmadığı için",
                    Points = 25
                }
            }
            );
            AddBookIfNotExists(dbContext,
            new Book
            {
                Title = "Komşu Gezegenler, Güneş'in Renkli Dansçıları",
                Category = "Uzayın Gizemleri",
                Description = @"Gökyüzüne bakmayı çok seven hayalperest Lina’nın odasında bulduğu sihirli bir müzik kutusuyla Güneş Sistemi'ne yaptığı büyüleyici yolculuğa hazır mısınız? Bu renkli macerada çocuklar; Güneş'in etrafında dönen komşu gezegenlerin muhteşem dansını, Satürn'ün buzdan halkalarını ve Mars'ın neden kıpkırmızı göründüğünü eğlenerek keşfedecekler.",
                Content = @"Dışarıda yağmurun usulca cama vurduğu, serin bir pazar günüydü. Sekiz yaşındaki Lina, odasında yere uzanmış, uzayla ilgili en sevdiği resimli kitabın sayfalarını çeviriyordu. O sırada yatağının altında, daha önce hiç fark etmediği küçük, ahşap bir kutu gördü. Üzerinde altın rengi yıldız işlemeleri olan, eski ve gizemli bir müzik kutusuydu bu.

Lina merakla kutuyu eline aldı ve kenarındaki küçük kolu yavaşça çevirmeye başladı. _Tık, tık, tık..._

Kutu aniden açıldı ve içinden pırıl pırıl, rengârenk ışıklar odaya yayıldı! Tatlı, huzur veren bir melodi çalmaya başladı. Odanın tavanı bir anda karanlık uzaya dönüştü ve Lina'nın etrafında, havada süzülen parlayan küreler belirdi. Lina sihirli bir uzay haritasının tam ortasındaydı!

Sahnenin en ortasında, kocaman, sıcacık ve ışıl ışıl parlayan dev bir ateş topu vardı.

""Merhaba Güneş!"" diye fısıldadı Lina gülümseyerek. Odanın içi bile hafifçe ısınmıştı. Güneş o kadar büyüktü ki, etrafındaki diğer küreler onun yanında minicik misketler gibi kalıyordu. Bütün bu misketler, yani gezegenler, kendi etraflarında dönerek Güneş'in çevresinde harika bir çember çiziyorlardı. Adeta Güneş'in etrafında dans ediyorlardı.

**Sence bütün bu gezegenler uzayda neden dağılıp gitmiyor da hep Güneş'in etrafında, hiç yorulmadan dönüyor olabilirler?**

Bunun sebebi Güneş'in görünmez kucaklaması, yani yerçekimidir! Güneş o kadar büyüktür ki, yerçekimi gücüyle bütün komşu gezegenleri kendine çeker ve onların kaybolmasını engeller.

Lina dans eden gezegenleri yakından incelemeye başladı. Güneş'e en yakın olan, aynı zamanda en minik olan gri bir gezegendi. O kadar hızlı koşuyordu ki, Lina onu gözleriyle zor takip ediyordu.

""Sen Merkür olmalısın!"" dedi Lina. Merkür, Güneş'e çok yakın olduğu için onun etrafındaki turunu herkesten hızlı bitiren, şampiyon bir koşucuydu. Ancak Güneş'e çok yakın olduğu için gündüzleri fırın gibi sıcak, geceleri ise buz gibi soğuk olurdu.

Merkür'ün hemen arkasında, pırıl pırıl parlayan sarımsı bir gezegen daha süzülüyordu. Bu Venüs'tü! Venüs o kadar parlaktı ki, gökyüzünde yıldızlara benzerdi. Ama üzeri kalın, battaniye gibi bulutlarla kaplıydı ve bu yüzden içerisi Güneş Sistemi'nin en sıcak yeriydi. Lina ona dokunmaya çalıştı ama Venüs sıcak ve kalın bulutlarıyla nazlı nazlı dönerek uzaklaştı.

Sonra Lina'nın gözü, dans eden gezegenler arasında en güzel, en tanıdık olana takıldı. Mavi denizleri, yeşil ormanları ve beyaz bulutlarıyla bir inci gibi parlıyordu.

""Burası bizim evimiz, Dünya!"" diyerek sevinçle zıpladı Lina. Dansçılar arasında üzerinde su, ağaçlar ve canlılar olan tek eşsiz gezegen Dünya'ydı. Lina kendi evini uzaydan izlemenin ne kadar harika bir duygu olduğunu düşündü.

Dünya'nın hemen peşinden, pas renginde, turuncu ve kırmızı karışımı küçük bir gezegen geliyordu.

**Sence uzayın ortasında duran Mars, neden paslı bir demir gibi kıpkırmızı görünüyordur? Acaba birisi onu kırmızıya mı boyadı?**

Elbette hayır! Mars'ın kırmızı görünmesinin sebebi, toprağında bulunan çok fazla miktardaki demir tozudur. Tıpkı yağmurda unutulmuş eski bir bisikletin paslanıp kızarması gibi, Mars'ın yüzeyi de demir yüzünden kırmızıya dönmüştür. Bu yüzden ona ""Kızıl Gezegen"" derler. Üstelik Mars'ın üzerinde, güneş sistemindeki en yüksek yanardağlar bulunur!

Lina müzik kutusunun melodisine kapılmışken, sahneye aniden dev gibi bir gezegen girdi. Bu, üzerinde sarı, turuncu ve kahverengi çizgiler olan Jüpiter'di. Bütün gezegenlerin en büyüğü, en ağabeyiydi! O kadar kocaman ve ağırdı ki, dönerken hafifçe uğulduyordu. Lina, Jüpiter'in üzerinde devasa, kırmızı bir leke gördü. Bu leke, yüzlerce yıldır devam eden kocaman bir rüzgâr fırtınasıydı.

Hemen arkasından ise Güneş Sistemi'nin en süslü dansçısı süzülerek geldi: Satürn! Satürn'ün belinde, pırıl pırıl parlayan kocaman bir hulahop vardı.

**Peki, Satürn'ün etrafındaki bu muhteşem parlayan halkalar sence neyden yapılmış olabilir? Uzaylıların fırlattığı altın tozlardan mı?**

Hayır, çok daha ilginci! Satürn'ün o eşsiz halkaları, uzayda savrulan irili ufaklı buz parçalarından ve taşlardan oluşur! Bu buz parçaları Güneş'in ışığını ayna gibi yansıttığı için uzaktan bakıldığında pırıl pırıl bir yüzük gibi görünür.

Dansın en arkasında ise, mavimsi renkleriyle birbirine çok benzeyen iki buzlu kardeş sessizce dönüyordu: Uranüs ve Neptün. Onlar Güneş'e o kadar uzaktılar ki, oralar her zaman çok soğuk, çok karanlık ve fırtınalıydı. Onlar grubun en ağırbaşlı dansçılarıydı.

Müzik kutusunun melodisi yavaş yavaş yavaşladı... Işıklar usulca sönmeye, gezegenler renkli birer sis bulutuna dönüşüp kaybolmaya başladı. Sonunda odaya tekrar yağmurun o tatlı sesi doldu.

Lina sihirli ahşap kutuyu nazikçe kapattı. Artık gökyüzüne her baktığında sadece parlayan beyaz noktalar görmeyecekti. O noktaların her birinin ayrı bir rengi, ayrı bir hikâyesi ve Güneş'in etrafında hiç bitmeyen harika bir dansı olduğunu biliyordu. Kendi etrafında kollarını açarak bir kez döndü, Dünya gibi... Gezegenlerin dansı, artık onun da kalbinde dönmeye devam edecekti.

##### Bugün Ne Öğrendik?

- **Merkezdeki Güç:** Güneş, gezegenlerin etrafında döndüğü devasa bir yıldızdır ve yerçekimi sayesinde bütün gezegenleri bir arada tutar.
    
- **Hızlı ve Sıcak:** Merkür Güneş'e en yakın ve etrafında en hızlı dönen gezegendir. Venüs ise kalın bulutları yüzünden sistemin en sıcak gezegenidir.
    
- **Kızıl Gezegen:** Mars'ın toprağında çok fazla demir bulunduğu için uzaydan bakıldığında paslı, kırmızımsı bir renkte görünür.
    
- **Gezegenlerin Kralı:** Jüpiter, Güneş Sistemi'ndeki en büyük gezegendir ve üzerinde çok uzun zamandır devam eden devasa kırmızı bir fırtına lekesi bulunur.
    
- **Buzdan Halkalar:** Satürn, etrafında pırıl pırıl parlayan buz ve taş parçalarından oluşmuş muhteşem halkalara (yüzüklere) sahiptir.",
                UnlockCost = 100,
                IsLocked = true
            },
            new List<Question>
            {
                new Question{
                    Text = "Hikâyemize göre gezegenlerin uzayda kaybolmadan Güneş'in etrafında dönebilmelerini sağlayan görünmez güç nedir?",
                    Options = new List<string>
                    {
                        "Rüzgâr gücü",
                        "Uzaylıların ipleri",
                        "Yerçekimi (Güneş'in kucaklaması)",
                        "Mıknatıs tozu"
                    },
                    CorrectAnswer = "Yerçekimi (Güneş'in kucaklaması)",
                    Points = 25
                },
                new Question{
                    Text = "Dünyamızın komşusu olan Mars gezegenine renginden dolayı hangi isim verilmiştir?",
                    Options = new List<string>
                    {
                        "Mavi Okyanus",
                        "Kızıl Gezegen",
                        "Yeşil Orman",
                        "Sarı Top"
                    },
                    CorrectAnswer = "Kızıl Gezegen",
                    Points = 25
                },
                new Question{
                    Text = "Güneş Sistemi'ndeki en kocaman, diğer bütün gezegenlerin ağabeyi sayılan gezegen hangisidir?",
                    Options = new List<string>
                    {
                        "Merkür",
                        "Venüs",
                        "Dünya",
                        "Jüpiter"
                    },
                    CorrectAnswer = "Jüpiter",
                    Points = 25
                },
                new Question{
                    Text = "Güneş'in etrafında dönerken belinde buz ve taşlardan oluşan pırıl pırıl bir hulahop (halka) taşıyan süslü gezegen hangisidir?",
                    Options = new List<string>
                    {
                        "Satürn",
                        "Uranüs",
                        "Neptün",
                        "Merkür"
                    },
                    CorrectAnswer = "Satürn",
                    Points = 25
                }
            }
            );
            AddBookIfNotExists(dbContext,
            new Book
            {
                Title = "Hızlı Roketler, Yıldızlara Uçan Gezginler",
                Category = "Uzayın Gizemleri",
                Description = @"Karton kutulardan kendi uzay gemisini yapan minik Mert’in uçsuz bucaksız yıldızlara doğru kurduğu bu heyecan verici hayale katılmaya ne dersiniz? Bu eğlenceli hikâyede çocuklar; dev roketlerin dünyanın görünmez çekim gücünden nasıl kurtulduğunu, uzaya çıkarken neden parçalara ayrıldıklarını ve cesur astronotların roketin neresinde yolculuk ettiğini keşfedecekler.",
                Content = @"Güneşli bir hafta sonu sabahıydı. Yedi yaşındaki Mert, odasının ortasına kocaman karton kutular dizmişti. Ellerinde yapıştırıcı, renkli bantlar ve alüminyum folyolar vardı. Mert uzayı, yıldızları ve gizemli gezegenleri çok severdi. En büyük hayali bir gün astronot kıyafetini giyip gökyüzünün en tepesine, hatta bulutların bile ötesine uçmaktı.

Karton kutuları dikkatlice üst üste koydu, en tepesine huni şeklinde bir şapka yaptı ve etrafını parlak gümüş folyoyla kapladı. İşte karşısında kendi yaptığı devasa uzay roketi duruyordu! Adını ""Yıldız Tozu"" koydu.

Mert büyük bir heyecanla roketinin içine girdi. Önüne çizdiği renkli düğmelere basıyormuş gibi yaptı. ""Üç, iki, bir... Ateşle!"" diye bağırdı. Ama Yıldız Tozu olduğu yerde duruyordu.

O sırada odanın kapısı aralandı. Mert’in uzay bilimlerine çok meraklı olan ablası Zeynep içeri girdi. Mert’in harika roketini görünce kocaman gülümsedi. ""Yıldız Tozu uçuşa hazır görünüyor Kaptan Mert!"" dedi. ""Peki gerçek roketlerin gökyüzüne nasıl fırladığını, o uzun yolculuğa nasıl çıktıklarını biliyor musun?""

Mert merakla başını salladı. ""Sanırım altlarından kocaman alevler çıkıyor ve _Vuuuuuşş!_ diye hızla uçuyorlar.""

""Çok doğru,"" dedi ablası yatağın kenarına oturarak. ""Peki sence roketlerin bu kadar büyük bir ateşe ve güce neden ihtiyacı vardır? Biz havaya zıpladığımızda gökyüzüne uçup gitmiyoruz, hemen geri yere düşüyoruz değil mi?""

**Sence bizi ve dünyadaki her şeyi yerde tutan, zıpladığımızda uzaya uçup kaybolmamızı engelleyen o görünmez güç nedir?**

Mert biraz düşündü, ellerini çenesine koydu. ""Yerçekimi!"" dedi heyecanla.

""Harika!"" dedi Zeynep. ""Dünyamız, üzerindeki her şeye görünmez bir sevgiyle, sımsıkı sarılır ve onları kendine çeker. İşte yerçekimi budur. Gerçek roketlerin dünyamızın bu güçlü kucağından kurtulup uzaya çıkabilmesi için çok ama çok hızlı, çok güçlü olmaları gerekir. Bu yüzden alt kısımlarında tonlarca özel yakıt bulunur. Motorlar ateşlendiğinde, o ateş devasa bir güçle aşağıya doğru itilir ve roket ok gibi gökyüzüne fırlar!""

Mert, roketinin tepesindeki parlak huniye dokundu. ""Peki abla, o kadar büyük ve ağır roketin hepsi mi uzaya, o uzak gezegenlere gidiyor?""

""İşte uzay roketlerinin en büyük sırrı da burada gizli,"" diye fısıldadı Zeynep gizemli bir ses tonuyla.

**Sen yokuş yukarı koşarken sırtındaki içi taş dolu ağır bir çantayı yere bıraksan daha mı hızlı koşarsın, yoksa daha mı yavaş?**

""Tabii ki daha hızlı koşarım! Yüküm hafifler, kuş gibi olurum,"" dedi Mert hemen.

""Aynen öyle Kaptan Mert! Roketler de uzaya çıkarken hızlanmak ve enerjilerini korumak için hafiflemek zorundadır. Bu yüzden o gördüğümüz kocaman roket, aslında iç içe geçmiş birkaç bölümden oluşur. En alttaki büyük yakıt deposunun içindeki yakıt bittiğinde, roket o ağır ve boş parçayı gökyüzünde bırakır. O parça gövdeden ayrılır ve roket daha da hafifleyerek hızla yoluna devam eder. Biraz daha yükselince, diğer parça da ayrılır...""

Mert şaşkınlıkla gözlerini kocaman açtı. ""Eğer parçalar ayrılıp düşüyorsa, geriye sadece neresi kalıyor? Astronotlar nerede duruyor?""

Zeynep parmağıyla Mert'in yaptığı roketin en tepesindeki sivri şapkayı gösterdi. ""Sadece en tepedeki küçücük oda kalıyor! O odaya 'kapsül' denir. O cesur astronotlar, roketin en uç noktasındaki o minicik kapsülün içinde otururlar. Altlarındaki tüm o devasa gövde ve yakıt depoları, sadece o küçük kapsülü yıldızlara güvenle ulaştırmak için vardır.""

Mert, kartondan yaptığı roketin en tepesine, yani kendi kapsülüne sevgiyle baktı. Gerçek roketlerin dünyadan ayrılmak için ne kadar büyük bir çaba harcadığını, uzaya varmak için nasıl yüklerinden kurtulup hafiflediklerini hayal etti. Kendini o minik kapsülün içindeki mutlu bir kâşif gibi hissetti.

Camdan dışarı, masmavi gökyüzüne baktı. Bir gün o da gerçekten o devasa alevlerin üzerinde, dünyanın görünmez kucağından kurtulup yıldızların arasına süzülecekti.

""Teşekkür ederim Zeynep,"" dedi Mert kocaman gülümseyerek. ""Şimdi Yıldız Tozu ile uzaya çıkmaya çok daha hazırım!""

Kapsülüne geri oturdu, gözlerini kapattı. İçindeki hayal gücü o kadar kuvvetliydi ki, odasının tavanından çoktan yıldızlara doğru süzülmeye başlamıştı bile.

##### Bugün Ne Öğrendik?

- **Görünmez Kucaklama:** Bizi ve nesneleri dünyada tutan güce yerçekimi denir. Roketler bu güçten kurtulmak için çok büyük bir hızla fırlatılırlar.
    
- **İtiş Gücü:** Roketlerin içinde tonlarca özel yakıt bulunur. Bu yakıt ateşlenip aşağı doğru püskürtüldüğünde roket zıt yöne, yani yukarıya fırlar.
    
- **Hafifleme Taktikleri:** Roketler uzaya çıkarken hızlanmak için altlarındaki boşalan yakıt depolarını (parçalarını) bırakarak hafiflerler.
    
- **Küçük Ev (Kapsül):** Astronotlar roketin devasa gövdesinde değil, sadece en tepesinde bulunan ""Kapsül"" adındaki çok özel ve korunaklı küçük odada yolculuk yaparlar.",
                UnlockCost = 150,
                IsLocked = true
            },
            new List<Question>
            {
                new Question{
                    Text = "Dünyamızın eşyaları ve bizi yere doğru çeken, roketlerin de uzaya çıkmak için yenmesi gereken gücün adı nedir?",
                    Options = new List<string>
                    {
                        "Rüzgar",
                        "Yerçekimi",
                        "Mıknatıs",
                        "Bulut itmesi"
                    },
                    CorrectAnswer = "Yerçekimi",
                    Points = 25
                },
                new Question{
                    Text = "Roketler gökyüzüne doğru uçarken daha çok hızlanabilmek için ne yaparlar?",
                    Options = new List<string>
                    {
                        "Paraşüt açarlar",
                        "Güneşlenmek için dururlar",
                        "Boşalan yakıt parçalarını gökyüzünde bırakarak hafiflerler",
                        "Su püskürtürler"
                    },
                    CorrectAnswer = "Boşalan yakıt parçalarını gökyüzünde bırakarak hafiflerler",
                    Points = 25
                },
                new Question{
                    Text = "Mert, kartondan yaptığı roketine hangi ismi vermiştir?",
                    Options = new List<string>
                    {
                        "Yıldız Tozu",
                        "Gümüş Ay",
                        "Ateş Topu",
                        "Bulut Gemisi"
                    },
                    CorrectAnswer = "Yıldız Tozu",
                    Points = 25
                },
                new Question{
                    Text = "Hikâyemize göre astronotlar, roketin hangi bölümünün içinde oturarak uzaya seyahat ederler?",
                    Options = new List<string>
                    {
                        "Roketin en altındaki motorun içinde",
                        "Roketin tekerleklerinde",
                        "Roketin dışındaki kanatlarda",
                        "Roketin en tepesindeki \"Kapsül\" adı verilen küçük odada"
                    },
                    CorrectAnswer = "Roketin en tepesindeki \"Kapsül\" adı verilen küçük odada",
                    Points = 25
                }
            }
            );
            #endregion

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