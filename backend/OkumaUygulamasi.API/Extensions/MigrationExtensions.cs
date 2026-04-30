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
                    Title = "Ayasofya, Gökyüzüne Dokunan Dev",
                    Category = "İstanbulun Mirasları",
                    Description = @"Küçük gezgin Elif ve tonton anneannesinin peşine takılarak İstanbul'un en görkemli yapısı Ayasofya'nın sırlarını keşfetmeye ne dersiniz? Bu sıcak ve merak uyandırıcı hikâyede çocuklar; devasa kubbenin altında geçmişe yolculuk yapacak, parlayan mozaiklerin, sevimli kedilerin ve sihirli dilek sütununun gizemini eğlenerek öğrenecekler.",
                    Content = @"Sultanahmet Meydanı'nda güneşli, harika bir gündü. Güvercinler _“Gurr, gurr”_ diyerek etrafta dolaşıyor, ağaçlardaki rengârenk çiçekler etrafa mis gibi kokular yayıyordu. Yedi yaşındaki Elif, elindeki simit kırıntılarını kuşlara atarken birden başını kaldırdı. Karşısında öyle büyük, öyle görkemli bir bina duruyordu ki gözlerine inanamadı.

Binanın rengi çok tatlı bir pembeydi ve en tepesinde kocaman, yuvarlak bir kubbesi vardı. Yanında ise gökyüzüne uzanan dört tane uzun minare, sanki binayı koruyan muhafızlar gibi nöbet tutuyordu.

Elif, hemen yanında duran, beyaz saçlı ve çok güzel masallar anlatan anneannesinin elini tuttu. ""Anneanne, şu karşıdaki devasa bina bir devin evi mi? Kapıları o kadar büyük ki, oradan ancak kocaman bir dev geçebilir!"" dedi gülerek.

Anneannesi sevgiyle gülümsedi ve Elif'in saçlarını okşadı. ""O bir devin evi değil güzel kızım,"" dedi. ""O, İstanbul'un kalbi sayılan, dünyanın en eski ve en özel binalarından biri: Ayasofya. Hadi, gel seninle onun gizemli dünyasına adım atalım.""

Birlikte yavaş yavaş Ayasofya'ya doğru yürüdüler. Çok eski ahşap ve tunç kapılardan içeri girdiklerinde Elif derin bir nefes aldı. İçerisi o kadar serin, o kadar genişti ki, Elif başını yukarı kaldırdığında şapkası neredeyse arkaya düşecekti.

Tepelerinde kocaman, sanki gökyüzünde asılı kalmış gibi duran bir kubbe vardı.

**Sence sadece taş, tuğla ve kum kullanarak, düşmeden havada duran bu kadar büyük bir kubbeyi o zamanlar nasıl yapmışlardır?** Eminim sen de o zamanın mimarlarının ne kadar zeki olduğunu düşünmüşsündür!

Anneannesi fısıldayarak anlatmaya başladı: ""Biliyor musun Elif, Ayasofya çok ama çok yaşlı bir yapıdır. Neredeyse 1500 yıl önce yapılmış! Üstelik bu kadar büyük olmasına rağmen yapımı sadece beş yıl sürmüş. Dünyanın dört bir yanından, en güzel mermerleri ve taşları buraya getirmişler.""

Elif içeride yürürken ayaklarının altındaki pürüzsüz mermerlere, duvarlardaki altın gibi parlayan desenlere baktı. Etrafta devasa avizeler asılıydı. Işıkları yandığında içerisi yıldızlı bir gece gibi görünüyordu. Elif o sırada, kalın bir taşın dibinde kıvrılmış mışıl mışıl uyuyan tüylü, sevimli bir kedi gördü. Ayasofya o kadar güvenli bir yerdi ki, kediler bile burayı evi gibi seviyordu.

""Anneanne, bu duvarlar neden bu kadar parlak?"" diye sordu Elif merakla gözlerini duvarlardan ayırmadan.

""Çünkü onlara mozaik deniyor canım,"" diye cevap verdi anneannesi. ""Minicik, renkli ve üzeri altınla kaplı cam parçalarını yan yana getirerek harika resimler yapmışlar. Güneş ışığı içeri vurdukça bu yüzden ışıl ışıl parlıyorlar.""

Biraz daha ilerlediklerinde, insanların kenardaki bir sütunun önünde toplandığını gördüler. Sütunun üzerinde küçük bir delik vardı ve insanlar sırayla o deliğe parmaklarını sokuyordu.

Sence insanlar bu taş sütunun önünde neden sıraya girmiş olabilirler? Acaba gizli bir oyun mu oynuyorlar?

Anneannesi, Elif'in meraklı bakışlarını fark etti. ""İşte burası Ayasofya'nın ünlü Dilek Sütunu,"" dedi göz kırparak. ""Buna Terleyen Direk de derler. Eski bir efsaneye göre, insanlar başparmaklarını bu deliğe sokup ellerini hiç kaldırmadan tam bir tur döndürebilirlerse dilekleri kabul olurmuş. Üstelik bu taşın eskiden insanlara şifa verdiğine inanılırmış.""

Elif heyecanla zıpladı. ""Ben de denemek istiyorum!""

Sıra onlara geldiğinde Elif minik başparmağını deliğe yerleştirdi. Dilini hafifçe dışarı çıkarıp odaklandı ve elini dikkatlice çevirmeye çalıştı. _Hopp!_ Tam bir tur döndürmüştü! İçinden çok güzel bir dilek tuttu. Belki yeni bir macera kitabı, belki de yeni bir keşif yolculuğu... Bunu sadece o ve Ayasofya biliyordu.

Dilek sütunundan sonra Ayasofya'nın içinde biraz daha dolaştılar. Pencerelerden süzülen güneş ışıkları, içerideki devasa hat levhalarının –yani üzerinde çok güzel yazılar olan yuvarlak dev panoların– üzerine düşüyordu. Elif bu levhaların ne kadar büyük olduğunu görünce onlara hayran kaldı.

Elif oradan hiç ayrılmak istemedi. Yüzlerce yıl önce yaşamış çocukların da aynı taşlarda koştuğunu hayal etti. Sanki Ayasofya, biriktirdiği anıları usulca fısıldıyordu.

Dışarı çıktıklarında güneş onlara gülümsüyordu. Elif dönüp o devasa pembemsi yapıya tekrar baktı. Artık onun bir devin evi olmadığını, ama gerçekten ""dev gibi"" bir tarihe sahip olduğunu biliyordu.

Anneannesine sarılarak, ""Teşekkür ederim anneanne,"" dedi. ""Ayasofya masallardaki saraylardan bile daha güzelmiş!""

Ayasofya, rüzgârın sesiyle sanki ""Yine gel küçük gezgin,"" diyerek Elif'e veda etti. Elif, bir sonraki macerasını hayal ederek anneannesinin elinden sıkıca tuttu ve evin yolunu tuttular.

##### Bugün Ne Öğrendik? 

**Yaşı ve Yapımı:** Ayasofya yaklaşık 1500 yıllık devasa bir yapıdır ve inşaatı sadece 5 yıl gibi çok kısa bir sürede tamamlanmıştır.
    
**Büyük Kubbe:** Ayasofya'nın en ünlü özelliği, o kadar eski olmasına rağmen havada asılı gibi duran devasa ve görkemli kubbesidir.
    
**Mozaik Sanatı:** Duvarlarındaki ışıltılı resimler; minik, renkli ve altın kaplı cam parçalarının yan yana getirilmesiyle oluşan mozaik sanatıyla yapılmıştır.
    
**Dilek Sütunu:** İçerisinde insanların başparmaklarını çevirerek dilek tuttukları, ""Terleyen Direk"" adı verilen gizemli bir sütun bulunur.
    
 **Kedilerin Evi:** Ayasofya kocaman bir yapı olmasının yanında, aynı zamanda sevimli kedilerin de çok sevdiği güvenli bir yuvadır.",
                    UnlockCost = 100,
                    IsLocked = true
                },
                new List<Question>
                {
                    new Question{ 
                        Text = "Ayasofya'nın devasa büyüklükteki yapımı toplam kaç yıl sürmüştür?", 
                        Options = new List<string>
                        {
                            "100 yıl ", 
                            "50 yıl", 
                            "5 yıl", 
                            "10 yıl"
                        },
                        CorrectAnswer = "5 yıl",
                        Points = 25
                    },
                    new Question{
                        Text = "Ayasofya'nın duvarlarındaki minik ve parlak cam parçalarının yan yana gelmesiyle yapılan süslemelere ne ad verilir?",
                        Options = new List<string>
                        {
                            "Sulu boya",
                            "Mozaik",
                            "Yapboz",
                            "Çizgi film"
                        },
                        CorrectAnswer = "Mozaik",
                        Points = 25
                    },
                    new Question{
                        Text = "Hikâyemize göre insanların sıraya girip başparmaklarını sokarak dilek tuttukları yerin adı nedir?",
                        Options = new List<string>
                        {
                            "Dilek Sütunu (Terleyen Direk)",
                            "Sihirli Kapı",
                            "Uçan Halı",
                            "Konuşan Duvar"
                        },
                        CorrectAnswer = "Dilek Sütunu (Terleyen Direkt)",
                        Points = 25
                    },
                    new Question{
                        Text = "Elif, Ayasofya'ya uzaktan baktığında onu ilk olarak neye benzetmiştir?",
                        Options = new List<string>
                        {
                            "Kocaman bir dağa",
                            "Bir uzay gemisine",
                            "Bir devin evine",
                            "Büyük bir oyun parkına"
                        },
                        CorrectAnswer = "Bir devin evine",
                        Points = 25
                    }

                }
                );

            AddBookIfNotExists(dbContext,
                new Book
                {
                    Title = "Galata Kulesi, Gökyüzüne Uzanan Şapkalı Dev",
                    Category = "İstanbulun Mirasları",
                    Description = @"Meraklı Zeynep ve annesinin dar, taşlı sokaklardan geçerek gökyüzüne uzanan Galata Kulesi’ni keşfetme macerasına davetlisiniz! Bu sıcacık hikâyede çocuklar; sivri şapkalı bu dev kulenin heyecan verici tarihini, Hezarfen Ahmet Çelebi’nin uçuş efsanesini ve İstanbul’un eşsiz manzarasını eğlenerek öğrenecekler.",
                    Content = @"Ilık bir bahar sabahıydı. Ağaçların dallarında taze yeşil yapraklar yeni yeni yüzünü gösteriyor, neşeli serçeler daldan dala atlayarak şarkı söylüyordu. Sekiz yaşındaki Zeynep, elinde küçük kırmızı fotoğraf makinesiyle İstanbul'un en hareketli, en renkli sokaklarından birinde yürüyordu. Yanında, ona her zaman yeni yerler keşfetmeyi öğreten annesi vardı.

Burası Beyoğlu'ydu. Yerdeki taşlar yapboz parçaları gibi yan yana dizilmişti. Sokağın köşesinde mışıl mışıl uyuyan sarman bir kedi, yanından geçen simitçinin ""Taze simit!"" sesiyle tek gözünü açıp geri kapattı. Zeynep etrafı izlemeye o kadar dalmıştı ki, başını kaldırıp ileriye baktığında olduğu yerde kalakaldı.

Binaların arasından gökyüzüne doğru uzanan kocaman, yuvarlak ve taştan bir dev onlara bakıyordu! Dev yapının en üstünde ise tıpkı masallardaki büyücülerin taktığına benzeyen sivri bir şapka vardı.

Zeynep gözlerini kocaman açtı. ""Anneciğim, bak! Şurada binaların arkasında saklanan şapkalı dev de neyin nesi?"" diye sordu heyecanla.

Annesi Zeynep'in şaşkın yüzüne bakıp kıkırdadı. ""O saklanan bir dev değil tatlım, İstanbul'un en yaşlı ve en bilge kulesi. Adı Galata Kulesi. Hadi, yakından bakalım mı?""

Birlikte dar sokaklardan kıvrılarak kuleye doğru yürümeye başladılar. Yürüdükçe kule büyüyor, büyüyor, gökyüzünü kaplıyordu. Zeynep kuleye yaklaştığında başını iyice geriye atıp en tepeye bakmak zorunda kaldı. Kule o kadar yüksek, o kadar görkemliydi ki Zeynep kendini bir an karınca kadar küçük hissetti.

**Sence yüzlerce yıl önce, günümüzdeki gibi kocaman elektrikli vinçler, kamyonlar yokken insanlar bu kadar ağır taşları üst üste dizip gökyüzüne kadar nasıl çıkmışlardır?** Eminim sen de bu işin çok büyük bir yardımlaşma ve akıl gerektirdiğini düşünüyorsundur!

Kulenin devasa ahşap kapısından içeri girdiklerinde Zeynep’i serin ve gizemli bir hava karşıladı. İçerideki taş duvarlar sanki geçmişte yaşanan maceraları fısıldıyordu. Annesi biletleri alırken Zeynep duvarlara dokundu.

""Biliyor musun Zeynep,"" dedi annesi, ""bu kule o kadar eskidir ki, yüzlerce yıl önce Cenevizliler adında denizci bir halk tarafından yapılmış. O zamanlar İstanbul'a denizden yaklaşan gemileri, tehlikeleri veya korsanları erkenden görebilmek için burayı bir gözetleme kulesi olarak inşa etmişler.""

Zeynep kuleyi yapan denizcileri hayal etti. Sonra asansöre bindiler. Asansör _""Vıııııızz""_ diye hafif bir ses çıkararak yukarı doğru çıkmaya başladı. Zeynep’in içi kıpır kıpır oldu. Sanki sihirli bir kutunun içinde gökyüzüne tırmanıyorlardı.

Yukarı çıkarken annesi anlatmaya devam etti: ""Yıllar sonra bu kule çok önemli bir görev daha üstlenmiş. İstanbul'da eski zamanlarda ahşap evler çokmuş ve bazen yangın çıkarmış. Şehrin her yerini gören bu kuleye, gözleri çok iyi gören nöbetçiler yerleştirilmiş. Bir yerde duman gördüklerinde hemen kocaman davullara vurup insanlara haber verirlermiş.""

**Peki, sence bugün kulede hâlâ yangınları gözetleyen nöbetçiler var mıdır, yoksa günümüzde yangınları haber vermenin daha hızlı yolları mı var?** Evet, artık telefonlarımız ve itfaiye alarmlarımız var! Bu yüzden kule şimdi yangın nöbeti tutmak yerine, senin gibi meraklı çocukları ve dünyadan gelen misafirleri ağırlıyor.

Asansör durduğunda, dar bir taş merdivenden birkaç basamak daha çıktılar. Ve işte! Kulenin en üstündeki o sivri şapkanın hemen altındaki açık balkona varmışlardı. Zeynep balkona adım attığında yüzüne ılık bir rüzgâr çarptı.

Manzara karşısında nefesi kesilmişti. Bütün İstanbul devasa bir tablo gibi ayaklarının altındaydı. Masmavi deniz güneşte parlıyor, vapurlar _""Vap! Vap!""_ diye seslenerek köpüklü yollar çiziyordu. Minik oyuncak arabalara benzeyen araçlar köprülerin üzerinden geçiyordu. Zeynep kendini kollarını açsa gökyüzünde uçacak bir kuş gibi özgür hissetti.

Tam bu sırada annesi Zeynep’in yanına eğilip usulca konuştu: ""Kuşlar gibi uçmak dedin de, sana kulenin en büyük sırrını vereyim mi? Yüzlerce yıl önce burada, Hezarfen Ahmet Çelebi adında çok zeki bir mucit yaşarmış. Kuşların nasıl uçtuğunu günlerce, aylarca incelemiş. Sonunda kendine tıpkı kuşlar gibi kocaman kanatlar yapmış. Bu kanatları sırtına takmış, işte tam bulunduğumuz bu noktadan kendini rüzgârın kollarına bırakmış.""

Zeynep'in ağzı açık kaldı. ""Gerçekten uçmuş mu anne? Düşmemiş mi?""

""Efsaneye göre uçmuş Zeynepciğim,"" diye gülümsedi annesi. ""Rüzgâr onu sırtlamış, kuşlar ona eşlik etmiş ve taa denizin karşı kıyısına, Üsküdar'a kadar süzülerek uçmuş.""

Zeynep hayranlıkla denizin karşısına baktı. Uçan bir insan fikri kalbini hızlandırmıştı. Gözlerini karşı kıyıya diktiğinde, denizin tam ortasında küçücük bir adanın üstünde duran o tanıdık yapıyı gördü.

""Anne bak! Kız Kulesi de orada!"" diye bağırdı parmağıyla göstererek. Galata Kulesi'nden Kız Kulesi o kadar tatlı, o kadar güzel görünüyordu ki... Zeynep hemen kırmızı fotoğraf makinesini yüzüne kaldırdı, _""Klik!""_ diye bu güzel anın fotoğrafını çekti. Ardından denizin ortasındaki Kız Kulesi'ne kocaman el salladı. ""Merhaba eski dost!"" diye fısıldadı.

Galata Kulesi, Zeynep'e rüzgârıyla sıcacık bir sarılma hediye etti. Zeynep o gün, geçmişin ne kadar sihirli olduğunu ve İstanbul'un şapkalı devinin dünyadaki en güzel manzaraya sahip olduğunu öğrenmişti. Kuleye veda ederken, içindeki keşfetme duygusu çoktan yeni maceralar için kanatlanıp uçmaya başlamıştı.

##### Bugün Ne Öğrendik? 

**Yapım Yılı ve Yapanlar:** Galata Kulesi, çok uzun zaman önce, 1348 yılında denizci bir halk olan Cenevizliler tarafından yapılmıştır.

**Eski Görevleri:** Kule ilk yapıldığında şehri ve denizdeki gemileri gözetlemek için, daha sonraları ise yüksekliği sayesinde **yangınları erken fark edip haber vermek** için kullanılmıştır.

**Uçan Mucit:** Çok zeki bir mucit olan Hezarfen Ahmet Çelebi’nin, kendi yaptığı kanatları takarak Galata Kulesi'nden İstanbul Boğazı'nın karşı kıyısına kadar uçtuğu anlatılır.

**Görünümü:** Kulenin en üst kısmı, uzaktan bakıldığında tıpkı kurşun rengi, sivri bir büyücü şapkasına benzer.",
                    UnlockCost = 50,
                    IsLocked = true
                },
                new List<Question>
                {
                    new Question{
                        Text = "Hikâyemizde Zeynep, kuleye yaklaştığında kulenin en üst kısmını neye benzetmiştir?",
                        Options = new List<string>
                        {
                            "Kocaman bir şemsiyeye",
                            "Büyücülerin taktığı sivri bir şapkaya",
                            "Düz bir kutuya",
                            "Ters dönmüş bir bardağa"
                        },
                        CorrectAnswer = "Büyücülerin taktığı sivri bir şapkaya",
                        Points = 25
                    },
                    new Question{
                        Text = "Galata Kulesi eskiden hangi amaçla kullanılmış?",
                        Options = new List<string>
                        {
                            "Okul olarak çocuklara ders vermek için",
                            "Hastane olarak insanları iyileştirmek için",
                            "Şehri denizden gelecek tehlikelere karşı gözetlemek ve yangınları haber vermek için",
                            "Fabrika olarak oyuncak üretmek için"
                        },
                        CorrectAnswer = "Şehri denizden gelecek tehlikelere karşı gözetlemek ve yangınları haber vermek için",
                        Points = 25
                    },
                    new Question{
                        Text = "Kendi yaptığı kanatlarla kuleye çıkıp uçtuğu söylenen ünlü mucit kimdir?",
                        Options = new List<string>
                        {
                            "Hezarfen Ahmet Çelebi",
                            "Piri Reis",
                            "Mimar Sinan",
                            "Evliya Çelebi"
                        },
                        CorrectAnswer = "Hezarfen Ahmet Çelebi",
                        Points = 25
                    },
                    new Question{
                        Text = "Zeynep kulenin balkonuna çıkınca denizdeki kime/neye el sallamıştır?",
                        Options = new List<string>
                        {
                            "Yunus balıklarına",
                            "Gökyüzündeki uçaklara",
                            "Karşı kıyıdaki ormana",
                            "Denizin ortasında duran Kız Kulesi'ne"
                        },
                        CorrectAnswer = "Denizin ortasında duran Kız Kulesi'ne",
                        Points = 25
                    }
                }
                );

            AddBookIfNotExists(dbContext,
                new Book
                {
                    Title = "Kız Kulesi, Denizin Ortasındaki Sır",
                    Category = "İstanbulun Mirasları",
                    Description = @"Can ve dedesiyle İstanbul Boğazı'nda keyifli bir yolculuğa çıkıp Kız Kulesi'nin sırlarını keşfetmeye hazır mısınız? Bu sıcacık hikâye; çocukları Kız Kulesi'nin büyüleyici tarihi, gizemli efsaneleri ve Galata Kulesi'yle olan kadim dostluğuyla tanıştırıyor.",
                    Content = @"Güneşli, pırıl pırıl bir İstanbul sabahıydı. Gökyüzünde pamuk gibi beyaz bulutlar yavaşça süzülüyor, martılar “Simiiit, simiiit!” der gibi neşeyle çığlıklar atarak uçuyordu. Yedi yaşındaki Can, dalgaların sesini dinlemeyi ve denizi izlemeyi çok severdi. O gün, en sevdiği oyun arkadaşı olan dedesiyle birlikte Üsküdar sahilinde yürüyüşe çıkmıştı.

Sahilde yürürken Can’ın gözü birden denizin tam ortasında duran, küçücük bir adanın üzerindeki yapıya takıldı. Dalgalar bu yapının etrafında nazlı nazlı köpürüyor, rüzgâr etrafında dönerek esiyordu.

Can, gözlerini kocaman açarak parmağıyla denizi işaret etti. “Dede, bak! Denizin tam ortasında küçücük bir ada var. Üzerinde de masallardaki gibi bir şato duruyor! Oraya nasıl gitmişler? İnsanlar denizin ortasına nasıl ev yapabilir ki?”

Dedesi Can’ın bu heyecanlı haline gülümsedi. Beyaz bıyıklarını sıvazladı ve, “Orası bir şato değil Can’cığım. Orası İstanbul’un en kıymetli, en zarif incisi: Kız Kulesi. Madem bu kadar merak ettin, hadi gel seninle küçük bir keşif yolculuğuna çıkalım!” dedi.

Can sevinçten havaya zıpladı. Birlikte sahildeki küçük iskeleye yürüdüler ve ahşap bir motorlu tekneye bindiler. Tekne “Pır pır pır…” diye sesler çıkararak mavi suların üzerinde kaymaya başladı. Rüzgâr Can’ın saçlarını uçuruyor, yüzüne hafifçe serin su damlaları sıçrıyordu. Burnuna mis gibi yosun kokusu geliyordu.

Kuleye doğru yaklaşırken Can kuleyi daha dikkatli inceledi. Alt kısmı sağlam taşlardan yapılmıştı, üstünde ise şapkayı andıran şirin bir çatı vardı.

**Sence denizin ortasında, suların üstünde duran kayalıkların üzerine böyle bir bina yapmak zor mudur?** Eminim sen de bunun çok dikkat ve çaba gerektiren bir iş olduğunu düşünmüşsündür.

Dedesi tekne ilerlerken anlatmaya başladı: “Biliyor musun Can, bu kule çok ama çok yaşlıdır. Neredeyse 2500 yaşında!”

Can duyduklarına inanamadı. “2500 mü?! Ama hiç de o kadar yaşlı görünmüyor. Nasıl bu kadar sağlam kalabilmiş?”

“Çünkü ona yıllar boyunca çok iyi bakılmış,” dedi dedesi. “Bu kule, sadece denizin ortasında durup etrafı izleyen bir süs değilmiş. Geçmişte çok önemli görevleri varmış. Eskiden burası bir gözetleme kulesiymiş. İstanbul’a gelen gemileri buradan izlerlermiş.”

Tekne kuleye iyice yaklaşmıştı. Can, kulenin en tepesindeki camlı bölüme baktı.

**Sence geceleri, her yer kapkaranlıkken ve gökyüzünde yıldızlar bile yokken, kocaman gemiler kayalıklara çarpmadan yollarını nasıl bulur?**

Evet, doğru tahmin ettin! Işıkla... Dedesi Can’ın düşünmesine fırsat verdikten sonra ekledi: “Kız Kulesi uzun yıllar boyunca bir deniz feneri olarak da çalışmış. Geceleri en tepesinde kocaman, parlak bir ateş yakarlarmış. Bu ışık, karanlıkta denizde yolculuk yapan gemilere ‘Buradayım, güvendesiniz, bana bakarak yolunuzu bulabilirsiniz’ dermiş. Kule, gemilerin en iyi arkadaşıymış.”

Can, kulenin aslında ne kadar yardımsever olduğunu öğrenince onu daha çok sevdi.

“Peki dede, neden adı Kız Kulesi? İçinde bir kız mı yaşamış?” diye sordu merakla.

Dedesi şefkatle gülümsedi. “Çok eski zamanlarda, İstanbul’da kızını dünyalar kadar çok seven bir kral yaşarmış. Kral, prensesi o kadar çok severmiş ki, onun için dünyanın en güvenli, en huzurlu ve en güzel yerini yapmak istemiş. Gürültüden uzak, sadece martı seslerinin olduğu, etrafı masmavi sularla çevrili bu kuleyi o prenses için inşa ettirmiş. Prenses burada martılara simit atar, her sabah dalgaların ninnisiyle uyanır, güven içinde denizi izlermiş.”

Can, prensesin denizin ortasında ne kadar huzurlu hissettiğini hayal etti. Kendisi de şu an teknede suların üzerinde çok güvende ve mutlu hissediyordu.

“Bir de karşıya bak bakalım Can,” dedi dedesi, parmağıyla denizin diğer tarafını, Avrupa yakasını göstererek. “Orada, tepenin üzerinde gökyüzüne uzanan büyük bir kule daha var. Görebildin mi?”

Can gözlerini kısıp baktı. “Evet, kocaman, sivri şapkalı bir kule!”

“İşte onun adı da Galata Kulesi. Kız Kulesi ile Galata Kulesi, İstanbul boğazının iki farklı yakasından birbirlerine bakan iki eski dost gibidirler. Biri denizin ortasında zarifçe durur, diğeri tepeden tüm şehri izler. Geceleri ışıkları yandığında, sanki birbirleriyle gizli gizli konuşurlar.”

Bu sırada tekne yavaşça adanın iskelesine yanaştı. “Güm!” diye hafifçe lastiklere çarparak durdu. Can büyük bir heyecanla karaya, yani denizin ortasındaki bu minik adaya ayak bastı.

Kulenin içine girdiklerinde serin, taştan duvarlar onları karşıladı. Dar ve dönen merdivenlerden yavaş yavaş, döne döne yukarı tırmandılar. En tepeye, o camlı bölüme çıktıklarında Can gördüğü manzara karşısında büyülenmişti.

Bütün İstanbul ayaklarının altındaydı. Bir yanda Topkapı Sarayı, diğer yanda vapurlar, köprü ve uzakta onlara göz kırpan Galata Kulesi... Denizin kokusu buradan daha da güzel geliyordu.

Can derin bir nefes aldı ve dedesine sarıldı. “İyi ki beni buraya getirdin dede. Kız Kulesi sadece taşlardan yapılmış eski bir bina değilmiş. O, hem prensesin güvenli evi, hem gemilerin yol göstericisi, hem de Galata Kulesi’nin en iyi arkadaşıymış!”

Kız Kulesi, güneşte parlayan camlarıyla sanki Can’ın bu sözlerini duymuş ve ona sevgiyle gülümsemişti. O günden sonra Can, ne zaman sahile inip denize baksa, denizin ortasındaki bu sihirli dostuna hep el salladı.

##### Bugün Ne Öğrendik? 

**Yaşı:** Kız Kulesi, yaklaşık 2500 yıllık tarihiyle İstanbul’un en eski ve özel yapılarından biridir.

**Yeri:** Üsküdar’ın Salacak sahilinde, denizin ortasındaki küçücük bir adanın üzerine inşa edilmiştir.

**Görevleri:** Yüzyıllar boyunca sadece güzel görünmekle kalmamış; gözetleme kulesi ve gece gemilere yol gösteren bir deniz feneri olarak kullanılmıştır.,,

**İsminin Hikâyesi:** Eski bir efsaneye göre, kızını çok seven bir kral, onu güven içinde yaşatmak için bu kuleyi yaptırmıştır.

**Kulelerin Dostluğu:** Kız Kulesi, denizin karşı kıyısında yer alan Galata Kulesi ile yüzyıllardır birbirine bakan iki eski dost gibidir.",
                    UnlockCost = 0,
                    IsLocked = false
                },
                new List<Question>
                {
                    new Question{
                        Text = "Hikâyemize göre Kız Kulesi denizin neresinde bulunmaktadır?",
                        Options = new List<string>
                        {
                            "Denizin en derin, karanlık dibinde ",
                            "Kumsalda, kumların tam üzerinde",
                            "Denizin ortasındaki küçücük bir adanın üzerinde",
                            "Dağların en yüksek zirvesinde"
                        },
                        CorrectAnswer = "Denizin ortasındaki küçücük bir adanın üzerinde",
                        Points = 25
                    },
                    new Question{
                        Text = "Kule, geceleri karanlıkta denizde yolculuk yapan gemilere nasıl yardım ediyormuş?",
                        Options = new List<string>
                        {
                            "Onlara yüksek sesle şarkı söyleyerek",
                            "En tepesinde büyük bir ateş/ışık yakarak",
                            "Gemilerin etrafına halatlar bağlayarak",
                            "Denizin suyunu ısıtarak"
                        },
                        CorrectAnswer = "En tepesinde büyük bir ateş/ışık yakarak",
                        Points = 25
                    },
                    new Question{
                        Text = "Kız Kulesi'nin İstanbul boğazının diğer yakasında bulunan, sivri şapkalı en iyi dostu hangi kuledir?",
                        Options = new List<string>
                        {
                            "Galata Kulesi",
                            "Saat Kulesi",
                            "Eyfel Kulesi",
                            "Beyaz Kule"
                        },
                        CorrectAnswer = "Galata Kulesi",
                        Points = 25
                    },
                    new Question{
                        Text = "Kız Kulesi konuşabilseydi, yanından geçen gemilere ilk olarak ne söylerdi?",
                        Options = new List<string>
                        {
                            "Lütfen beni de yanınızda götürün!",
                            "Dikkatli olun, ışığımı takip ederseniz güvende olursunuz!",
                            "Denizin suyu bugün çok soğuk, yüzmeyin.",
                            "Burada martılarla oynamak çok sıkıcı."
                        },
                        CorrectAnswer = "Dikkatli olun, ışığımı takip ederseniz güvende olursunuz!",
                        Points = 25
                    }
                }
                );

            AddBookIfNotExists(dbContext,
                new Book
                {
                    Title = "Yerebatan Sarnıcı, Yerin Altındaki Saklı Orman",
                    Category = "İstanbulun Mirasları",
                    Description = @"Sıcak bir yaz gününde, meraklı Ali ve maceracı teyzesiyle birlikte yerin altındaki serin ve gizemli bir sarayı keşfetmeye hazır mısınız? Bu heyecan verici hikâyede çocuklar; Yerebatan Sarnıcı'nın sudan yükselen yüzlerce sütununu, efsanevi Medusa başlarını ve karanlık sularda sessizce yüzen balıkları eğlenerek öğrenecekler.",
                    Content = @"Sıcak bir Temmuz öğleden sonrasıydı. Güneş, İstanbul'un sokaklarını fırın gibi ısıtmıştı. Yedi yaşındaki Ali, elindeki erimeye başlayan limonlu dondurmasını kurtarmaya çalışarak Defne teyzesinin peşinden yürüyordu. Defne teyzesi tarih kokan yerleri gezmeyi ve saklı sırları ortaya çıkarmayı çok severdi.

Sultanahmet Meydanı'nda yürürken teyzesi aniden durdu. Ali'ye dönüp, ""Bu sıcaktan kaçıp, yerin altındaki serin ve gizli bir saraya gitmeye ne dersin?"" diye sordu göz kırparak.

Ali'nin gözleri kocaman oldu. Yerin altında bir saray mı? Hem de serin? ""Evet, evet, gidelim!"" diye zıpladı.

Birlikte tuğladan yapılmış küçük bir binadan içeri girdiler. Biletlerini aldıktan sonra, aşağıya doğru uzanan taş merdivenlerden yavaş yavaş inmeye başladılar. Aşağı indikçe hava serinliyor, dışarıdaki o sıcak ve gürültülü şehirden adım adım uzaklaşıyorlardı.

**Sence yerin altı neden her zaman dışarıdaki havadan daha serin olur?** Eminim sen de toprağın güneşi içeri almadığını, bu yüzden bir battaniye gibi orayı serin tuttuğunu tahmin etmişsindir!

Ali merdivenlerin sonuna geldiğinde gördüğü manzara karşısında ağzı şaşkınlıktan ""O"" harfi gibi açıldı. Karanlık, sarımsı tatlı bir ışıkla aydınlatılmış koca bir salonun içindeydi. Ama bu salonun zemini taş değil, pırıl pırıl suydu!

Suyun içinden gökyüzüne, yani tavana doğru uzanan yüzlerce taş direk vardı. Bu direkler sanki taştan ağaçlara benziyordu, burası yer altındaki bir orman gibiydi.

""Teyze, burası gerçekten bir saray!"" diye fısıldadı Ali. Sesinin kocaman tavanlarda hafifçe yankılanması çok hoşuna gitmişti.

Defne teyzesi gülümsedi. ""İnsanlar buraya çok görkemli olduğu için 'Yerebatan Sarayı' demeyi çok sever. Ama buranın gerçek adı Yerebatan Sarnıcı. Sarnıç, su deposu demektir Ali'ciğim. Çok eski zamanlarda, İstanbul'un padişahları ve halkı susuz kalmasın diye yapılmış. Yağmur sularını ve çok uzaklardaki ormanlardan gelen suları burada, yerin altında biriktirirlermiş.""

Ali, suyun üzerinde uzanan güvenli ahşap yollarda yürümeye başladı. Su o kadar durgundu ki, sütunların yansıması bir ayna gibi suya düşüyordu. Tam o sırada suda hareket eden kırmızımsı bir şey gördü.

""Teyze bak, balık!"" diye bağırdı heyecanla. Evet, bu karanlık ve serin suların içinde irili ufaklı balıklar yüzüyordu. Kimi sakince süzülüyor, kimi ahşap yolun altından geçip kayboluyordu. Sarnıcın sessiz ve sevimli bekçileri gibiydiler.

Biraz ilerlediklerinde, diğerlerinden çok daha farklı görünen bir sütun buldular. Üzerinde sanki gözyaşı döküyormuş gibi oymalar vardı ve bu sütun hep ıslaktı.

""Buna 'Ağlayan Sütun' veya 'Gözyaşı Sütunu' diyorlar,"" dedi teyzesi. ""Efsaneye göre, bu sarnıcı inşa ederken çok yorulan işçilerin anısına ağlıyormuş."" Ali, minik parmağıyla sütunun üzerindeki damla şekillerine dokundu. Sütunun ne kadar vefalı olduğunu düşündü.

Ahşap yolda yürümeye devam ettiler. Sarnıcın en köşesine, en kuytu ve gizemli yerine doğru yaklaşıyorlardı. Teyzesi, ""Şimdi sana buranın en büyük sırrını göstereceğim,"" dedi heyecanlı bir sesle.

Suyun içinden yükselen iki kısa sütunun tam altına baktılar. Sütunların altında destek olarak kullanılan, taştan yapılmış iki kocaman yüz duruyordu! Ama bunlar düz durmuyordu; biri yan yatmış, diğeri ise tamamen ters çevrilmişti.

**Sence bu devasa taş yüzler neden düz değil de, birisi yan diğeri ise ters duracak şekilde konulmuş olabilir?**

""Bunlar efsanevi yılan saçlı kadın Medusa'nın başları,"" diye açıkladı teyzesi gülümseyerek. ""Neden ters ve yan konuldukları tam bir sır! Kimisi o zamanlar eski inanışlardaki kötü ruhları uzaklaştırmak için ters koyduklarını söyler, kimisi de sütunların boyuna tam uysun diye inşaat ustalarının böyle yerleştirdiğini düşünür. Belki de eski ustalar sadece bizim kafamızı karıştırmak istemiştir, ne dersin?""

Ali kıkırdadı. Ters duran taş yüze el salladı. Hikâyelerdeki canavarlar hiç de korkutucu değildi burada. Aksine çok ilginçti. Medusa sanki saklambaç oynarken taşların arkasında yakalanmış gibi duruyordu.

Yerebatan Sarnıcı'nın serin, sessiz ve büyülü dünyasında biraz daha dolaştılar. Ali, yerin altında böyle muazzam bir ormanın, suyun ve tarihin saklı olmasına inanamıyordu. Dışarı çıkmak için merdivenleri tırmanırken, yukarıda, yollarda yürüyen insanların ayaklarının altında nasıl bir mucize yattığından habersiz olduklarını düşündü. Ama o artık bu sırrı biliyordu.

Sokağa çıkıp güneşi tekrar gördüklerinde Ali teyzesine sıkıca sarıldı. ""Burası benim en sevdiğim gizli karargâhım oldu teyze! Acaba İstanbul'un altında daha başka hangi sırlar saklı?""

Defne teyzesi güldü, ""Onu da bir sonraki maceramızda keşfederiz!""

##### Bugün Ne Öğrendik? 

**Sarnıç Ne Demek?:** Yerebatan Sarnıcı, çok eski zamanlarda şehrin su ihtiyacını karşılamak için yapılmış yer altındaki devasa bir su deposudur.

**Diğer Adı:** İçinde tam 336 tane sütun bulunduğu ve çok görkemli olduğu için halk arasında ""Yerebatan Sarayı"" olarak da bilinir.

**Suyun Misafirleri:** Bu kocaman yer altı havuzunun sularında yaşayan, sarnıcın bekçileri gibi yüzen sevimli balıklar vardır.

**Ağlayan Sütun:** Sarnıçtaki sütunlardan birinin üzerinde gözyaşı damlasına benzeyen şekiller vardır ve üzeri hep ıslak olduğu için ona ""Ağlayan Sütun"" denir.

**Medusa Başları:** Sarnıcın en köşesinde, sütunların altında destek olarak kullanılan biri ters, diğeri yan duran iki tane gizemli ve efsanevi Medusa heykeli bulunur.",
                    UnlockCost = 150,
                    IsLocked = true
                },
                new List<Question>
                {
                    new Question{
                        Text = "Hikâyemize göre Yerebatan Sarnıcı aslında hangi amaçla inşa edilmiştir?",
                        Options = new List<string>
                        {
                            "Kralın yazın serin serin uyuması için ",
                            "Şehrin ve sarayın su ihtiyacını karşılamak için",
                            "Müzik konserleri vermek için ",
                            "Şövalyelerin saklanması için"
                        },
                        CorrectAnswer = "Şehrin ve sarayın su ihtiyacını karşılamak için ",
                        Points = 25
                    },
                    new Question{
                        Text = "Sarnıcın içindeki suların içinde sessizce yüzen canlılar hangileridir?",
                        Options = new List<string>
                        {
                            "Kurbağalar",
                            "Su kaplumbağaları",
                            "Balıklar",
                            "Ördekler"
                        },
                        CorrectAnswer = "Balıklar",
                        Points = 25
                    },
                    new Question{
                        Text = "Üzerindeki oymalar yüzünden, sarnıcı yaparken yorulan işçiler için üzüldüğüne inanılan sütunun adı nedir?",
                        Options = new List<string>
                        {
                            "Gülen Sütun",
                            "Uyuyan Sütun",
                            "Ağlayan (Gözyaşı) Sütun",
                            "Saklanan Sütun"
                        },
                        CorrectAnswer = "Ağlayan (Gözyaşı) Sütun",
                        Points = 25
                    },
                    new Question{
                        Text = "Ali ve teyzesinin sarnıcın köşesinde buldukları, taştan yapılmış ters ve yan duran heykeller kimin başıdır?",
                        Options = new List<string>
                        {
                            "Yılan saçlı Medusa'nın ",
                            "Kocaman bir aslanın",
                            "Uçan bir ejderhanın",
                            "Eski bir kralın"
                        },
                        CorrectAnswer = "Yılan saçlı Medusa'nın",
                        Points = 25
                    }
                }
                );

            #endregion

        }
        private static void AddBookIfNotExists(AppDbContext dbContext, Book book, List<Question> questions)
        {
            if (!dbContext.Books.Any(b=> b.Title == book.Title))
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