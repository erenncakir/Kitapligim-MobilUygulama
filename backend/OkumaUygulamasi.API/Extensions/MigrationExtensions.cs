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
                    Title = "Neşeli Yunuslar, Suların Özgür Dansçıları",
                    Category = "Denizin Derinlikleri",
                    Description = @"Rüzgârı saçlarında hisseden küçük Kaan’ın yelkenli bir tekneyle mavi sulara açılıp neşeli yunuslarla tanışmasına eşlik etmeye hazır mısınız? Bu sıcacık hikâyede çocuklar; yunusların nefes alma sırlarını, karanlık sularda yönlerini nasıl bulduklarını ve uyurken neden tek gözlerini açık tuttuklarını macera dolu bir yolla öğrenecekler.",
                    Content = @"Güneşli, rüzgârın tatlı tatlı estiği bir pazar sabahıydı. Altı yaşındaki Kaan, hayatında ilk defa babasıyla birlikte küçük, beyaz bir yelkenli tekneyle denize açılmıştı. Deniz o kadar maviydi ki, sanki gökyüzü aşağıya inmiş de suya dönüşmüş gibiydi. Martılar teknenin peşinden uçuyor, rüzgâr Kaan'ın yanaklarını hafifçe okşuyordu.

Kaan teknenin ucuna oturdu, bacaklarını sallandırdı ve heyecanla denizi izlemeye başladı. Suyun altında kimlerin yaşadığını, orada nasıl bir hayat olduğunu çok merak ediyordu.

Bir ara, teknenin hemen ilerisinde suyun köpürdüğünü gördü. Sonra birden... _Şlapp!_

Sudan gri, parlak ve pürüzsüz bir canlı fırladı! Havada harika bir takla attı ve zarifçe tekrar suya daldı.

""Baba, bak! Kocaman bir balık uçtu!"" diye bağırdı Kaan gözlerine inanamayarak.

Babası yanına geldi ve denize bakıp gülümsedi. ""Çok şanslısın Kaan. Onlar balık değil, suların en neşeli dansçıları olan yunuslar. Bize eşlik etmek, bizimle oyun oynamak istiyorlar.""

Kaan şaşırmıştı. ""Nasıl yani? Suda yaşıyorlar, yüzgeçleri var ve harika yüzüyorlar ama balık değiller mi?""

Babası başını salladı. ""Hayır, onlar tıpkı bizim gibi memeli hayvanlardır. Yani suda yaşamalarına rağmen nefes almak için suyun dışına, yani havaya ihtiyaçları vardır.""

**Sence yunuslar nefes almak için bizim gibi burunlarını mı kullanırlar?**

Aslında hayır! Yunusların başlarının tam tepesinde sihirli bir kapak gibi çalışan küçük bir delik vardır. Su yüzeyine çıktıklarında bu delikten ""_Püfff!_"" diye güçlü bir şekilde içerideki eski havayı dışarı üfler ve saniyeler içinde tertemiz bir nefes alırlar. Suyun altına dalarken de bu sihirli kapak sımsıkı kapanır ve içlerine bir damla bile su kaçmaz!

Kaan dikkatle izlemeye devam etti. Teknenin yanında şimdi bir değil, tam üç tane yunus yüzüyordu. Sanki birbirleriyle yarışıyor, teknenin oluşturduğu dalgalarda ustaca sörf yapıyorlardı. Kaan onların bu neşeli ve oyuncu haline bayılmıştı.

O sırada yunuslardan incecik, müzik gibi sesler duymaya başladı. _İiii! Cik! Fiyuuuu!_

Yunuslar sanki bir şarkı söylüyor ya da birbirlerine bir şeyler anlatıyor gibiydiler.

""Babacığım, yunuslar birbirleriyle mi konuşuyor?"" diye sordu.

""Evet,"" dedi babası. ""Biliyor musun, yunusların tıpkı senin ve benim gibi kendilerine ait isimleri vardır. Ama onların isimleri harflerden değil, özel ıslıklardan oluşur. Her yunusun kendine ait farklı ve özel bir ıslığı vardır. Anne yunus yavrusunu çağırmak istediğinde, onun bu özel ıslığını çalarak ona seslenir.""

Kaan bunu duyunca kendi ismini ıslıkla nasıl çalabileceğini düşündü. Bu gerçekten çok eğlenceli ve zekiceydi! İsimlerini ıslıkla söyleyen sevimli canlılar...

Yunuslar teknenin önünde o kadar hızlı yüzüyorlardı ki, Kaan onların denizin çok derin ve karanlık yerlerinde kayalara çarpmadan nasıl yüzdüklerini merak etti. Gözlerini kıstı ve suyun içine doğru bakmaya çalıştı.

**Geceleri veya denizin çok derinlerinde her yer kapkaranlıkken, sence yunuslar yollarını nasıl bulurlar? Yanlarında minik bir el feneri taşıyamazlar, değil mi?**

İşte yunusların muazzam bir süper gücü daha: Sesle görmek! Buna 'ekolokasyon' denir. Yunuslar suyun içinde ""tık tık tık"" diye hızlı ses dalgaları gönderirler. Bu ses dalgaları gidip denizin altındaki bir kayaya veya balığa çarpar ve yankı olarak yunusa geri döner. Böylece yunus, gözleri tamamen kapalı olsa bile önünde ne olduğunu, ne kadar uzakta olduğunu ve hatta ne kadar büyük olduğunu anında anlar. Onlar karanlık suların en usta kâşifleridir!

Gün yavaş yavaş batmaya başlarken, güneş denizi pırıl pırıl bir turuncuya boyadı. Kaan bütün gün yunusları izlemekten biraz yorulmuş, tatlı tatlı esnemeye başlamıştı. Aklına birden çok ilginç bir soru geldi.

""Baba, peki yunuslar hiç uyumaz mı? Su yüzeyine nefes almak için çıkmak zorundalarsa, uyuduklarında bunu nasıl yapıyorlar?""

Babası Kaan'ın saçlarını okşadı. ""İşte yunusların en büyük sırrı budur Kaan'cığım. Onlar uyurken beyinlerinin sadece bir yarısı uyur, diğer yarısı ise her zaman uyanık kalır! Üstelik uyurken bir gözleri hep açıktır. Böylece uyanık kalan yarıları, onlara nefes almak için yüzeye çıkmalarını hatırlatır ve onları tehlikelerden korur.""

Kaan hayranlıkla yunuslara baktı. Yarısı uyuyan, yarısı uyanık kalan bir beyin... Gözleri açık uyuyan dansçılar... Denizin altı gerçekten de devasa bir mucizeydi!

Artık eve dönme vakti gelmişti. Yunuslar da bunu anlamış gibi teknenin etrafında son bir kez döndüler. İçlerinden bir tanesi havaya sıçradı, zarif kuyruğunu sallayarak sanki Kaan'a ""Hoşça kal küçük dostumuz!"" dedi ve mavi suların derinliklerine doğru süzülerek gözden kayboldu.

Kaan da onlara el salladı. Kalbi denizin bu muhteşem sırlarıyla sevgiyle çarpıyordu. Yunuslar ona denizin sadece kocaman bir su birikintisi değil, zeki, neşeli ve sevgi dolu canlıların evi olduğunu öğretmişti.,

##### Bugün Ne Öğrendik?

- **Yunuslar Memelidir:** Suda yaşamalarına ve balığa benzemelerine rağmen balık değillerdir, tıpkı bizim gibi akciğerleriyle nefes alan memeli hayvanlardır.
    
- **Tepedeki Nefes Deliği:** Nefes almak için burunlarını değil, başlarının en üstünde bulunan sihirli bir kapak gibi açılıp kapanan nefes deliklerini kullanırlar.
    
- **Islık İsimler:** Her yunusun kendine ait, imza niteliğinde özel bir ıslığı vardır ve birbirlerine bu ıslıklarla, yani isimleriyle seslenirler.
    
- **Sesle Görme (Ekolokasyon):** Karanlık sularda yönlerini bulmak ve avlanmak için etrafa ses dalgaları yayarlar, bu seslerin yankısıyla çevrelerini ""görürler"".
    
- **Yarım Uyku:** Yunuslar uyurken beyinlerinin sadece yarısı uyur ve bir gözleri daima açık kalır. Böylece boğulmazlar ve nefes almak için su yüzeyine çıkabilirler.",
                    UnlockCost = 0,
                    IsLocked = false
                },
                new List<Question>
                {
                    new Question{
                        Text = "Hikâyemize göre yunuslar nefes almak için vücutlarının neresini kullanırlar?",
                        Options = new List<string>
                        {
                            "Ağızlarını",
                            "Kuyruklarını",
                            "Başlarının üstündeki nefes deliğini",
                            "Gözlerini"
                        },
                        CorrectAnswer = "Başlarının üstündeki nefes deliğini",
                        Points = 25
                    },
                    new Question{
                        Text = "Yunuslar birbirlerine seslenmek veya isimlerini söylemek için hangi yöntemi kullanırlar?",
                        Options = new List<string>
                        {
                            "Mektup yazarlar",
                            "Kendilerine özel ıslıklar çalarlar",
                            "Kanat çırparlar",
                            "Davul çalarlar"
                        },
                        CorrectAnswer = "Kendilerine özel ıslıklar çalarlar",
                        Points = 25
                    },
                    new Question{
                        Text = "Karanlık denizlerde yunusların yollarını kaybetmemesini sağlayan süper güçleri (ekolokasyon) nasıl çalışır?",
                        Options = new List<string>
                        {
                            "Ses dalgaları gönderip yankısını dinleyerek",
                            "El feneri yakarak",
                            "Yıldızlara bakarak",
                            "Diğer balıklara yol sorarak"
                        },
                        CorrectAnswer = "Ses dalgaları gönderip yankısını dinleyerek",
                        Points = 25
                    },
                    new Question{
                        Text = "Yunusların uyku düzeni ile ilgili en ilginç özellik aşağıdakilerden hangisidir?",
                        Options = new List<string>
                        {
                            "Sadece kışın uyumaları",
                            "Uyurken deniz dibindeki kumlara saklanmaları",
                            "Uyurken beyinlerinin sadece yarısının uyuması ve bir gözlerinin açık kalması",
                            "Uyurken çok yüksek sesle horlamaları"
                        },
                        CorrectAnswer = "Uyurken beyinlerinin sadece yarısının uyuması ve bir gözlerinin açık kalması",
                        Points = 25
                    }
                }
                );
            AddBookIfNotExists(dbContext,
                new Book
                {
                    Title = "Akıllı Ahtapotlar, Derinlerin Gizemli Dahileri",
                    Category = "Denizin Derinlikleri",
                    Description = @"Küçük kâşif Deniz'in sarı denizaltısıyla okyanusun derinliklerine dalarak denizlerin en zeki saklambaç ustasıyla tanışmasına hazır mısınız? Bu eğlenceli macerada çocuklar; ahtapotların renk değiştirme sihrini, üç kalpli olmalarının sırrını ve sekiz kollu bu sevimli dahilerin şaşırtıcı zekâsını eğlenerek keşfedecekler.",
                    Content = @"Güneşli, harika bir gündü. Gökyüzü masmavi, deniz ise çarşaf gibi dümdüzdü. Deniz'in çok sevdiği, camları kocaman, pırıl pırıl sarı renkte minik bir denizaltısı vardı. Adı ""Köpük""tü. Deniz, Köpük ile okyanusun altına dalmayı ve yeni canlılarla tanışmayı her şeyden çok severdi.

Bugün Deniz'in gizli bir görevi vardı. Arkadaşı yunuslar ona denizlerin altında yaşayan çok zeki bir canlıdan bahsetmişti. Yunuslar ona, ""Derinlerin Gizemli Dahisi"" diyorlardı. Deniz, bu gizemli dahiyi bulmak için Köpük'ün direksiyonuna geçti ve denizaltının kapağını kapattı.

_Glu glu glu..._ Köpük, yavaşça suyun altına doğru süzülmeye başladı.

Suyun altı tıpkı sihirli bir ormana benziyordu. Rengârenk mercanlar, suyun akıntısıyla dans eden yeşil yosunlar ve sürü halinde gezen minik balıklar harika görünüyordu. Deniz, denizaltının farlarını yaktı ve büyük kayalıkların arasına doğru ilerledi. Her yer çok sessizdi.

Deniz, üzeri yosun tutmuş, gri ve pütürlü kocaman bir kayaya dikkatle bakıyordu. O sırada inanamayacağı bir şey oldu! Gri kaya birdenbire iki kocaman göz açtı ve Deniz'e bakıp göz kırptı!

**Sence hiç kıpırdamadan duran, sıradan gri bir kaya durup dururken birisine göz kırpabilir mi?**

Haklısın, tabii ki kayalar göz kırpmaz! Deniz şaşkınlıkla camdan bakarken, o taş zannettiği şey birdenbire değişmeye başladı. Önce gri rengi açıldı, sonra üzeri minik kırmızı ve turuncu beneklerle doldu. Altından uzun, kıvrımlı kollar çıktı. O bir kaya değildi; inanılmaz bir saklambaç ustası olan bir ahtapottu!

Ahtapot kollarını nazikçe sallayarak Köpük'ün camına yaklaştı. Sanki ""Merhaba, ben de seni bekliyordum!"" diyordu.

Ahtapotların tam sekiz tane uzun kolu vardır. Deniz, bu kolların altındaki minik, yuvarlak vantuzları, yani yapışkan düğmecikleri fark etti. Ahtapot, bu düğmecikler sayesinde hem kayalara sıkıca tutunabiliyor hem de nesneleri inceleyebiliyordu. Ama daha da ilginci, ahtapotlar kollarındaki bu düğmeciklerle dokundukları her şeyin tadını alabilirler!

**Düşünsene, parmaklarınla en sevdiğin çikolatalı pastaya sadece dokunarak onun ne kadar tatlı olduğunu anladığını! Sence de çok eğlenceli ve sihirli bir özellik değil mi?**

Deniz bu gizemli canlıyı hayranlıkla izlerken, ahtapot birden oyun oynamak istedi. İki kayanın arasındaki küçücük, incecik bir yarığa doğru yüzdü. Deniz kendi kendine, ""Eyvah! O kadar büyük ki oraya asla sığamaz, kesinlikle sıkışıp kalacak,"" diye düşündü. Ama ahtapot sanki sudan yapılmış gibi akıp o küçücük delikten kolayca geçiverdi.

Çünkü ahtapotların vücutlarında hiç kemik yoktur! İçlerinde onları sertleştiren bir iskelet bulunmadığı için, gözlerinin sığabildiği minicik deliklerden bile hiç zorlanmadan, kıvrılarak geçebilirler. Yumuşacık vücutları onların en büyük gücüdür.

Ahtapot deliğin diğer tarafından çıkıp neşeyle tekrar Deniz'in yanına geldi. Bu harika canlının sırları saymakla bitmiyordu. Bizim göğsümüzde ""güm güm"" atan sadece bir tane kalbimiz var, değil mi? Ama bu sualtı dahilerinin tam üç tane kalbi vardır! Evet, yanlış duymadın! Üç kalp, okyanusun derinliklerinde çok daha enerjik ve hızlı yüzmelerini sağlar.

**Peki, bizim dizimiz kanadığında kanımızın rengi kırmızı olur. Sence üç kalpli ahtapotların kanı ne renktir? Kırmızı mı, yeşil mi, yoksa mavi mi?**

Bunu tahmin etmek biraz zor olabilir. Ahtapotların kanı mavidir! Kanlarında bizimki gibi demir değil, bakır adı verilen bir madde bulunur. Bu da onları tıpkı masallardaki sihirli yaratıklara benzetir.

Derken, denizaltının ışığı kumların üzerinde parlayan bir nesneye takıldı. Bu, deniz dibine düşmüş kapalı bir cam kavanozdu. İçinde ise parlak, incili bir denizkabuğu vardı. Deniz, ahtapotun ne yapacağını merakla izledi. Ahtapot kavanoza yaklaştı. Önce sekiz koluyla kavanozu sıkıca sardı. Sonra kollarını ustaca kullanarak kavanozun kapağını tıpkı bizim gibi çevirmeye başladı. _Tık, tık, tık..._ ve kapak açıldı!

Evet, ahtapotlar kapalı kavanozları açabilecek, labirentleri geçebilecek ve zor bulmacaları çözebilecek kadar akıllıdırlar. İşte yunusların onlara ""Derinlerin Gizemli Dahileri"" demesinin sebebi buydu!

Ahtapot, içindeki parlak denizkabuğunu alıp kollarının arasında sevgiyle tuttu. Sonra Deniz'e dönüp rengini mutluluktan tatlı bir pembeye çevirdi.

Deniz, bu zeki, üç kalpli, mavi kanlı ve şekilden şekle giren arkadaşıyla tanıştığı için dünyanın en şanslı kâşifi gibi hissediyordu. Gitme vakti gelmişti. Denizaltının camından ona el salladı. Ahtapot da sekiz kolundan biriyle Deniz'e veda etti ve suyunu püskürterek hızla uzaklaştı. Birkaç saniye içinde tekrar bir kayanın rengini almış ve saklambaç oyununda görünmez olmuştu.

Deniz, sarı denizaltısı Köpük ile yukarı, Güneş'e doğru çıkarken kıkırdadı. Denizlerin altı gerçekten de devasa bir sihirbazlık sahnesiydi ve ahtapotlar bu sahnenin en büyük yıldızlarıydı.

##### Bugün Ne Öğrendik?

- **Sekiz Kol ve Tadım Sihri:** Ahtapotların sekiz tane kolu vardır ve bu kolların üzerindeki minik vantuzlar sayesinde dokundukları şeylerin tadını alabilirler.
    
- **Kemiksiz Vücut:** Vücutlarında hiç kemik olmadığı için, çok dar ve küçücük yerlerden bile sıvı gibi süzülerek geçebilirler.
    
- **Renkli Kamuflaj:** Ahtapotlar tehlikelerden saklanmak veya avlanmak için renklerini ve derilerinin şeklini saniyeler içinde değiştirerek bulundukları yere uyum sağlarlar.
    
- **Üç Kalp ve Mavi Kan:** İnanması güç olsa da ahtapotların tam üç tane kalbi vardır ve kanlarının rengi mavidir!
    
- **Derinlerin Dahileri:** Ahtapotlar okyanusun en zeki canlılarından biridir; kapalı kavanozların kapaklarını çevirip açabilir, bulmacaları çözebilirler.",
                    UnlockCost = 50,
                    IsLocked = true
                },
                new List<Question>
                {
                    new Question{
                        Text = "Hikâyemize göre ahtapotlar kollarının altındaki minik yapışkan düğmeler (vantuzlar) sayesinde ne yapabilirler?",
                        Options = new List<string>
                        {
                            "Çok hızlı koşabilirler",
                            "Dokundukları şeylerin tadını alabilirler",
                            "Şarkı söyleyebilirler",
                            "Ateş yakabilirler"
                        },
                        CorrectAnswer = "Dokundukları şeylerin tadını alabilirler",
                        Points = 25
                    },
                    new Question{
                        Text = "Ahtapotlar neden küçücük ve dar deliklerden bile kolayca sıkışmadan geçebilirler?",
                        Options = new List<string>
                        {
                            "Çünkü vücutlarında hiç kemik yoktur",
                            "Çünkü çok hızlıdırlar",
                            "Çünkü özel bir sabun kullanırlar",
                            "Çünkü görünmez olma güçleri vardır"
                        },
                        CorrectAnswer = "Çünkü vücutlarında hiç kemik yoktur",
                        Points = 25
                    },
                    new Question{
                        Text = "Ahtapotların vücudunda hayat suyu pompalayan kaç tane kalp bulunur?",
                        Options = new List<string>
                        {
                            "1",
                            "2",
                            "3",
                            "4"
                        },
                        CorrectAnswer = "3",
                        Points = 25
                    },
                    new Question{
                        Text = "Deniz'in okyanusta gördüğü ahtapot, kumların üzerindeki kapalı cam kavanozu bulunca zekâsını kullanarak ne yaptı?",
                        Options = new List<string>
                        {
                            "Kavanozu alıp denizin dibine gömdü",
                            "Kollarıyla kavanozun kapağını çevirerek açtı",
                            "Kavanozun üstüne oturup uyudu",
                            "Kavanozu Deniz'e fırlattı"
                        },
                        CorrectAnswer = "Kollarıyla kavanozun kapağını çevirerek açtı",
                        Points = 25
                    }
                }
                );
            AddBookIfNotExists(dbContext,
                new Book
                {
                    Title = "Sevimli Denizatları, Mercanların Minik Bekçileri",
                    Category = "Denizin Derinlikleri",
                    Description = @"Meraklı kâşif Ada’nın sihirli şnorkeliyle rengârenk mercan ormanlarına dalarak denizlerin en gizemli ve narin canlısıyla tanışmasına katılmaya ne dersiniz? Bu heyecan verici hikâyede çocuklar; babalarının karnında büyüyen yavruları, fırtınada akıntıya kapılmamak için mercanlara sarılan kıvrık kuyrukları ve dik yüzen denizatlarının sihirli dünyasını eğlenerek keşfedecekler.",
                    Content = @"Gözlerini kapat ve kendini masmavi, ılık bir denizin kıyısında hayal et. Su o kadar berrak ki, güneş ışıkları denizin dibindeki altın sarısı kumların üzerinde dans ediyor. Sekiz yaşındaki Ada, denizi çok seven, saçları rüzgârda dalgalanan neşeli ve minik bir kâşifti. Ada'nın en büyük hayali, okyanusun derinliklerindeki gizli dünyaları görmekti.

Bir sabah, en sevdiği pembe şnorkelini ve deniz gözlüğünü taktı. Suya usulca, ""_Cumburlop!_"" diye atladı.

Suyun altı bambaşka bir gezegen gibiydi! Mor, sarı ve kırmızı renklerde kocaman mercanlar tıpkı çiçek açmış ağaçlara benziyordu. Minik, gümüş renkli balıklar sürü halinde bir sağa bir sola hızla yüzüyordu. Ada, ellerini ve ayaklarını çırparak bu mercan ormanının içine doğru süzüldü.

Büyük, yeşil yosunlarla kaplı bir mercanın yanından geçerken aniden durdu. Gözlerine inanamıyordu! Yeşil yosunun bir parçası sanki yavaşça kımıldamıştı. Ada biraz daha yaklaştı. Yosun sandığı o incecik şeyin üzerinde fıldır fıldır dönen iki tane minik, siyah göz vardı!

**Sence sıradan bir yosunun etrafa merakla bakan boncuk gibi gözleri olabilir mi?**

Elbette olamaz! Ada, karşısında duran bu gizemli canlıyı dikkatle inceledi. Başı tıpkı minyatür bir ata benziyordu! Ağzı ise ince uzun bir pipet gibiydi. Ama en ilginci, vücudunun alt kısmıydı. Kuyruğu tıpkı ağaç dallarına tutunan sevimli bir maymunun kuyruğu gibi içe doğru kıvrıktı.

Ada kalbinin hızla çarptığını hissetti. O, efsanelerde duyduğu ama daha önce hiç görmediği bir canlıyı bulmuştu: Bir denizatı!

Bu minik canlı o kadar küçüktü ki, neredeyse Ada'nın eli kadardı. Üstelik yeşil yosunların arasında o kadar iyi saklanmıştı ki, rengini tıpkı yosunlar gibi yeşile çevirmişti. Denizatları tıpkı birer bukalemun gibi saklanmak istediklerinde renk değiştirebilirlerdi. Bu yüzden onlar mercanların minik, görünmez bekçileriydi.

Ada denizatının nasıl yüzeceğini çok merak etti. Onu izlemek için olduğu yerde sessizce bekledi.

**Düşünsene, balıklar denizde ok gibi yatay bir şekilde, dümdüz yüzerler. Sence denizatları da onlar gibi mi yüzer?**

Hayır! Denizatları diğer balıklar gibi yüzmezler. O, denizin içinde dimdik, tıpkı ayakta duran bir insan gibi yüzmeye başladı. Sırtındaki küçücük, şeffaf yüzgeci o kadar hızlı ""pır pır"" ediyordu ki, Ada bu yüzgeci bir helikopter pervanesine benzetti. Boynundaki minik yüzgeçler de ona yön veriyordu. Ama çok yavaş yüzüyordu.

Tam o sırada, denizin dibinde hafif bir akıntı başladı. Su, Ada'nın saçlarını geriye doğru dalgalandırdı. Denizatları çok iyi yüzücüler olmadıkları için bu akıntı onu uzaklara sürükleyebilirdi.

**Peki, suyun altında güçlü bir akıntı geldiğinde, bu minik ve narin canlı sürüklenmemek için sence ne yapmıştır?**

Ada hayranlıkla izledi. Denizatı hiç panik yapmadı. O kıvrık, maymun kuyruğuna benzeyen kuyruğunu hemen yanındaki sağlam bir mercan dalına sıkıca doladı! Tıpkı bizim rüzgârlı bir havada bir direğe sarılıp tutunmamız gibi... Artık güvendeydi. Su onu ne kadar iterse itsin, kuyruğu sayesinde mercandan ayrılmıyordu.

Denizatı mercana tutunurken bir yandan da karnını doyurmaya karar verdi. Etraftan geçen minicik su piresi gibi canlılara doğru uzun ağzını uzattı ve ""_Hüüp!_"" diye onları tıpkı bir elektrik süpürgesi gibi içine çekti. Biliyor musun, denizatlarının dişleri ve bizimki gibi yiyecekleri biriktiren bir mideleri yoktur. Yediklerini hemen sindirirler, bu yüzden sürekli, ama sürekli yemek yemek zorundadırlar.

Ada bu minik dostunu izlerken biraz ileride başka bir denizatı daha gördü. Ama bu denizatı diğerinden biraz farklıydı. Karnı kocaman, şişkinceydi! Sanki içine bir pinpon topu saklamış gibi görünüyordu.

**Doğada yavrularını karnında taşıyanlar genellikle annelerdir. Sence bu şişman karınlı denizatı anne miydi?**

İşte denizatlarının en büyük ve en sihirli sırrı buradaydı! Ada, sualtı belgesellerinde izlediği bir bilgiyi hatırladı: Denizatlarında yavruları anneler değil, babalar taşırdı! Evet, bu şişman karınlı olan bir baba denizatıydı. Babaların karnında tıpkı kangurularınki gibi güvenli bir kese bulunur. Anne denizatı yumurtaları bu keseye bırakır ve baba onları yavrular büyüyene kadar kendi karnında taşır.

Ada heyecanla nefesini tuttu, çünkü inanılmaz bir şeye şahit oluyordu. Baba denizatının karnı yavaşça kasıldı ve kesesinin içinden iğne ucu kadar küçük, şeffaf, minicik denizatları teker teker suya fırlamaya başladı! _Pıt... Pıt... Pıt..._

Onlarca minik yavru denizatı, babalarının karnından çıkıp anında dik bir şekilde yüzmeye ve kuyruklarıyla etraftaki minik yosunlara tutunmaya başladılar. Tıpkı babalarının kopyası gibiydiler ama çok daha küçüktüler.

Ada, bu sihirli anı hayatı boyunca hiç unutmayacağını biliyordu. Mercanların bu narin, zeki ve çalışkan bekçilerine usulca el salladı. Yavru denizatları da minik yüzgeçlerini çırparak sanki ona karşılık verdiler. Ada, içi sevgi ve mutlulukla dolarak güneşin parladığı su yüzeyine doğru yüzmeye başladı. Denizin derinlikleri, beklediğinden çok daha muhteşem sürprizlerle doluydu.

##### Bugün Ne Öğrendik?

- **Dik Yüzen Canlılar:** Denizatları diğer balıklar gibi yatay değil, suyun içinde dimdik (ayakta duruyormuş gibi) yüzerler.
    
- **Baba Kangurular:** Dünyada yavrularını karnında taşıyıp doğuran tek erkek canlı, baba denizatlarıdır. Babaların karnında yavruları büyüttükleri özel bir kese bulunur.
    
- **Kıvrık Kuyruk:** Denizatlarının maymun kuyruğuna benzeyen, bir şeyleri kavrayabilen güçlü kuyrukları vardır. Bu sayede akıntıda sürüklenmemek için mercanlara ve yosunlara sıkıca tutunurlar.
    
- **Dişsiz ve Midesiz:** Ağızları uzun bir pipete benzer. Dişleri ve mideleri olmadığı için avlarını elektrik süpürgesi gibi içlerine çekerler ve doymak için sürekli yemek zorundadırlar.
    
- **Bukalemun Gibi:** Tehlikelerden korunmak ve saklanmak için tıpkı bukalemunlar gibi renk değiştirebilirler.",
                    UnlockCost = 100,
                    IsLocked = true
                },
                new List<Question>
                {
                    new Question{
                        Text = "Hikâyemize göre denizatları suyun içinde nasıl yüzerler?",
                        Options = new List<string>
                        {
                            "Sırtüstü yatarak",
                            "Diğer balıklar gibi ok gibi yatay bir şekilde",
                            "Dimdik, ayakta duruyormuş gibi",
                            "Kendi etrafında taklalar atarak"
                        },
                        CorrectAnswer = "Dimdik, ayakta duruyormuş gibi",
                        Points = 25
                    },
                    new Question{
                        Text = " Suyun altında güçlü bir akıntı geldiğinde denizatları sürüklenmemek için ne yaparlar?",
                        Options = new List<string>
                        {
                            "Kumların altına saklanırlar",
                            "Maymun gibi kıvrık kuyruklarıyla mercanlara veya yosunlara tutunurlar",
                            "Çok hızlı yüzerek kaçarlar",
                            "Su yüzeyine çıkıp beklerler"
                        },
                        CorrectAnswer = "Maymun gibi kıvrık kuyruklarıyla mercanlara veya yosunlara tutunurlar",
                        Points = 25
                    },
                    new Question{
                        Text = "Denizatları yemeklerini nasıl yerler?",
                        Options = new List<string>
                        {
                            "Keskin dişleriyle ısırarak",
                            "Yemeklerini elleriyle tutarak",
                            "Pipete benzeyen ağızlarıyla içlerine hüpleterek (çekerek)",
                            "Yemeklerini taşlarla kırarak"
                        },
                        CorrectAnswer = "Pipete benzeyen ağızlarıyla içlerine hüpleterek (çekerek)",
                        Points = 25
                    },
                    new Question{
                        Text = "Denizatlarının doğadaki diğer birçok canlıdan ayrılan, yavru bakımıyla ilgili en ilginç özelliği nedir?",
                        Options = new List<string>
                        {
                            "Yavrularına şarkı söylemeleri",
                            "Yavruları annelerin değil, babaların kendi karnındaki kesede taşıması",
                            "Yavrularını deniz kabuklarının içine saklamaları",
                            "Yavrularını sadece geceleri beslemeleri"
                        },
                        CorrectAnswer = "Yavruları annelerin değil, babaların kendi karnındaki kesede taşıması",
                        Points = 25
                    }
                }
                );
            AddBookIfNotExists(dbContext,
                new Book
                {
                    Title = "Şarkı Söyleyen Balinalar, Okyanusun Dev Korosu",
                    Category = "Denizin Derinlikleri",
                    Description = @"Küçük kâşif Nil ve deniz bilimci amcasıyla birlikte uçsuz bucaksız okyanusta dünyanın en büyük müzik festivaline katılmaya ne dersiniz? Bu büyüleyici macerada çocuklar; devasa kambur balinaların okyanusun öbür ucuna kadar ulaşan sihirli şarkılarını, suyu nasıl püskürttüklerini ve kocaman karınlarını minicik deniz canlılarıyla nasıl doyurduklarını heyecanla keşfedecekler.",
                    Content = @"Denizin iyice sakinleştiği, gökyüzünün masmavi ve bulutsuz olduğu harika bir sabahtı. Yedi yaşındaki Nil, elindeki kocaman, beyaz denizkabuğunu kulağına dayamış, gözlerini kapatarak içinden gelen o derin _“Huuuu…”_ sesini dinliyordu. Nil müziği ve denizi dünyadaki her şeyden çok severdi.

O gün, deniz bilimci olan amcası Ozan ile birlikte ""Mavi Nota"" adındaki büyük bir araştırma gemisindeydiler. Kıyıdan çok uzaklaşmış, okyanusun engin sularına açılmışlardı.

Amcası Ozan, güvertede elinde kalın bir kabloyla Nil’in yanına geldi. ""Denizkabuğunun içindeki ses çok güzeldir Nil,"" dedi gülümseyerek. ""Ama bugün sana okyanusun gerçek şarkıcılarını, dünyanın en büyük korosunu dinleteceğim.""

Nil gözlerini kocaman açtı. ""Okyanusta bir koro mu var? Yani denizkızları mı şarkı söylüyor amca?"" diye sordu heyecanla.

Amcası kahkaha attı. ""Denizkızlarından çok daha büyük, çok daha görkemli şarkıcılar! Hadi, kulaklığını tak bakalım.""

Amcası, elindeki kablonun ucuna bağlı olan ağır, metal bir aleti yavaşça denizin içine doğru sarkıttı. ""Bu aletin adı hidrofon,"" diye açıkladı. ""Yani sualtı mikrofonu. Suyun altındaki en ufak fısıltıyı bile duymamızı sağlar.""

Nil, kocaman kulaklıkları takıp dikkatle dinlemeye başladı. Başta sadece suyun hafif şırıltısı ve minik tıkırtılar duyuluyordu. Her yer çok sessizdi. Nil tam ""Acaba şarkıcılar bugün izinli mi?"" diye düşünecekti ki...

Birden, kulaklıkların içinden inanılmaz bir ses yankılandı!

_“Uuuuuuu… Meeeeee… Oooooo…”_

Bu ses o kadar derin, o kadar güçlü ve yankılıydı ki Nil’in tüyleri diken diken oldu. Sanki devasa bir keman çalınıyor, aynı anda derinden bir flüt sesi geliyordu. Ses bir inip bir çıkıyor, adeta sihirli bir melodi oluşturuyordu.

""Amca! Bu... Bu harika bir ses! Kim söylüyor bu şarkıyı?"" diye fısıldadı Nil, şarkıcıyı ürkütmekten korkarak.

""İşte karşında okyanusun en büyük sanatçıları: Kambur balinalar!"" dedi amcası gururla. ""Şu an kilometrelerce uzaktaki arkadaşlarına, ailelerine şarkı söyleyerek sesleniyorlar. Biliyor musun Nil, bu şarkılar suyun içinde havadan çok daha hızlı ve uzağa gider. Bir balina şarkı söylediğinde, okyanusun öbür ucundaki bir başka balina onu duyabilir.""

**Düşünsene, sen odanda şarkı söylüyorsun ve taa başka bir şehirdeki arkadaşın seni duyabiliyor! Sence de balinaların sesi inanılmaz güçlü değil mi?**

Nil denizin yüzeyine bakarak bu dev şarkıcıları görmeye çalıştı. O sırada geminin biraz ilerisinde suyun yüzeyi kabarmaya, fokurdamaya başladı. Denizin içinden simsiyah, devasa bir karaltı yükseliyordu.

Ve aniden... _Svuuşşş!_

Sanki denizin içinden büyük bir denizaltı çıkıyormuş gibi kocaman, gri-siyah renkli bir kambur balina suyun yüzeyinde belirdi! Nil hayatında bu kadar büyük bir canlı görmemişti. Balina neredeyse büyük bir okul otobüsü kadardı!

Balinanın başının tam üstündeki deliklerden gökyüzüne doğru kocaman, köpüklü bir su fıskiyesi fışkırdı. _Püüüffff!_

**Sence bu kadar kocaman bir canlı, suların altında saatlerce kalıp nefesini nasıl tutuyordur? Balinalar da balık mıdır?**

Hayır, balinalar balık değildir! Onlar tıpkı yunuslar ve bizim gibi memeli canlılardır. Yani suyun altında nefes alamazlar. Taze hava solumak için suyun yüzeyine çıkmak zorundadırlar. Başlarının üstünde bulunan o delikler aslında onların burunlarıdır! Suyu püskürttüklerinde, içlerindeki eski havayı dışarı atarlar ve tertemiz bir nefes çekerler.

Nil, bu nazik devin suya tekrar yavaşça süzülüşünü izledi. ""Amca,"" dedi merakla, ""bu kadar kocaman bir hayvan doyabilmek için kim bilir ne kadar büyük balıklar, devasa köpekbalıkları yiyordur!""

Amcası Ozan başını iki yana sallayarak gülümsedi. ""İnanması çok güç ama, bu devasa balinalar okyanustaki en minicik canlılarla beslenirler! Adlarına 'Krill' denilen, senin serçe parmağın kadar küçük, karidese benzeyen minik canlıları yerler.""

Nil çok şaşırmıştı. Okul otobüsü büyüklüğünde bir hayvan, serçe parmağı kadar küçük bir şeyi yiyerek nasıl doyabilirdi?

""Kambur balinaların ağzında bizimki gibi dişler yoktur,"" diye devam etti amcası. ""Onun yerine 'balina bıyığı' denilen, kocaman, fırçaya benzeyen dişleri vardır. Balina kocaman ağzını açıp tonlarca suyu ve içindeki minik krilleri yutar. Sonra suyu bu fırça gibi bıyıkların arasından dışarı iter. Kriller ağzında kalır ve onları afiyetle midesine indirir. Bir kerede binlercesini yerler!""

Nil, okyanusun bu dev kütüphanesinde ne kadar çok sır saklı olduğunu bir kez daha anladı. Onlar okyanusun kaba devleri değil, minicik şeylerle beslenen, şarkı söyleyen zarif ve bilge canlılarıydı.

Bir süre sonra kambur balina, okyanusun derinliklerine dalmak için hazırlandı. Devasa, kelebek kanadına benzeyen kuyruğunu suyun yüzeyine çıkardı. Nil’e el sallar gibi kuyruğunu havaya kaldırdı ve suları köpürterek maviliklerin içine doğru sessizce kayboldu.

Ama şarkısı hidrofonun kulaklıklarından hâlâ gelmeye devam ediyordu. _“Ooooooo… Meeeeee…”_

Nil, güneş batarken kulaklıkları yavaşça başından çıkardı. Artık denize her baktığında sadece suyu değil, devasa ve görünmez bir konser salonunu görecekti. Çünkü şarkı söyleyen balinalar her zaman orada, derinlerin en güzel ninnilerini söylemeye devam edeceklerdi.

##### Bugün Ne Öğrendik?

- **Balinalar Balık Değildir:** Suyun içinde yaşamalarına rağmen, balinalar tıpkı bizim gibi akciğerleriyle nefes alan memeli hayvanlardır.
    
- **Tepedeki Burun (Nefes Deliği):** Nefes almak için su yüzeyine çıkarlar ve başlarının üzerindeki delikten eski havayı suyu püskürterek dışarı atarlar.
    
- **Okyanus Korosu:** Özellikle kambur balinalar, birbirleriyle iletişim kurmak, ailelerini bulmak için kilometrelerce öteye ulaşabilen çok güçlü ve melodik şarkılar söylerler.
    
- **Devasa Beden, Minik Yemek:** Çok büyük olmalarına rağmen dişleri yoktur. Bunun yerine fırça gibi olan ""balina bıyıklarıyla"" suyu süzerek ""Krill"" adı verilen minicik canlıları yiyerek beslenirler.
    
- **Sualtı Mikrofonu:** Suyun altındaki sesleri dinlemek ve kaydetmek için kullanılan özel aletlere ""Hidrofon"" denir.",
                    UnlockCost = 150,
                    IsLocked = true
                },
                new List<Question>
                {
                    new Question{
                        Text = "Hikâyemize göre balinalar birbirlerine mesaj göndermek ve iletişim kurmak için ne yaparlar?",
                        Options = new List<string>
                        {
                            "Mors alfabesiyle ışık yakarlar",
                            "Birbirlerine şarkı söylerler",
                            "Denizin dibindeki kayalara resim çizerler",
                            "Suyun üstüne zıplarlar"
                        },
                        CorrectAnswer = "Birbirlerine şarkı söylerler",
                        Points = 25
                    },
                    new Question{
                        Text = "Kambur balinaların midelerini doyurmak için yedikleri, serçe parmağı büyüklüğündeki minicik canlıların adı nedir?",
                        Options = new List<string>
                        {
                            "Krill",
                            "Su piresi",
                            "Deniz yıldızı",
                            "Hamsi"
                        },
                        CorrectAnswer = "Krill",
                        Points = 25
                    },
                    new Question{
                        Text = "Balinalar nefes almak için vücutlarındaki hangi bölümü kullanırlar?",
                        Options = new List<string>
                        {
                            "Kuyruklarındaki solungaçları",
                            "Karınlarındaki özel bir keseyi",
                            "Başlarının üstünde bulunan nefes deliğini (burunlarını)",
                            "Kulaklarını"
                        },
                        CorrectAnswer = "Başlarının üstünde bulunan nefes deliğini (burunlarını)",
                        Points = 25
                    },
                    new Question{
                        Text = " Nil ve amcası Ozan'ın suyun altındaki balina şarkılarını dinlemek için kullandıkları aletin (sualtı mikrofonu) adı nedir?",
                        Options = new List<string>
                        {
                            "Teleskop",
                            "Hidrofon",
                            "Stetoskop",
                            "Megafon"
                        },
                        CorrectAnswer = "Hidrofon",
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