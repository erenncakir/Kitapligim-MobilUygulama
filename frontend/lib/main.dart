import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:uuid/uuid.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  runApp(const KutuphaneApp());
}

class KutuphaneApp extends StatelessWidget {
  const KutuphaneApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Genel Kültür Kütüphanesi',
      debugShowCheckedModeBanner: false,
      theme: ThemeData(
        brightness: Brightness.dark,
        scaffoldBackgroundColor: const Color(0xFF121212), // Koyu antrasit arka plan
        appBarTheme: const AppBarTheme(
          backgroundColor: Color(0xFF1E1E1E),
          elevation: 0,
        ),
      ),
      home: const KutuphaneEkrani(),
    );
  }
}

class KutuphaneEkrani extends StatefulWidget {
  const KutuphaneEkrani({super.key});

  @override
  State<KutuphaneEkrani> createState() => _KutuphaneEkraniState();
}

class _KutuphaneEkraniState extends State<KutuphaneEkrani> {
  String _cihazKimligi = "";
  final String baseUrl = "https://okuma-uygulamasi-api.onrender.com";
  List<dynamic> _kitaplar = [];
  bool _yukleniyor = true;

  @override
  void initState() {
    super.initState();
    _sistemiBaslat();
  }

  Future<void> _sistemiBaslat() async {
    final prefs = await SharedPreferences.getInstance();
    String? deviceId = prefs.getString('deviceId');

    if (deviceId == null) {
      deviceId = const Uuid().v4();
      await prefs.setString('deviceId', deviceId);
    }

    setState(() {
      _cihazKimligi = deviceId!;
    });

    // Kullanıcıyı sisteme tanıtıyoruz
    await http.get(Uri.parse('$baseUrl/api/v1/users/$_cihazKimligi'));

    // Ardından kitapları çekiyoruz
    await _kitaplariGetir();
  }

  Future<void> _kitaplariGetir() async {
    try {
      final response = await http.get(Uri.parse('$baseUrl/api/v1/books'));
      if (response.statusCode == 200) {
        setState(() {
          // Gelen JSON verisini Flutter'ın anlayacağı listeye çeviriyoruz
          _kitaplar = json.decode(response.body);
          _yukleniyor = false;
        });
      }
    } catch (e) {
      setState(() {
        _yukleniyor = false;
      });
      print("Kitaplar çekilirken hata oluştu: $e");
    }
  }
  // Eren'in API'sine gidip jeton düşerek kilidi açan fonksiyon
  Future<void> _kilitAc(int bookId) async {
    try {
      final response = await http.post(
        Uri.parse('$baseUrl/api/v1/users/unlock-book?userId=$_cihazKimligi&bookId=$bookId'),
      );

      if (response.statusCode == 200) {
        // BAŞARILI: Perdeyi kapat ve ekrana güzel haberi ver
        if (mounted) Navigator.pop(context); // Önce perdeyi kapat

        _mesajGoster("Harika!", "Kitap kütüphanene eklendi. Keyifli okumalar!", true);

        await _kitaplariGetir(); // Listeyi yenile
      } else {
        // HATA: Jeton yetersiz veya sunucu hatası
        _mesajGoster("Hata", "Jetonun yetersiz olabilir veya bir sorun oluştu. (Kod: ${response.statusCode})", false);
      }
    } catch (e) {
      _mesajGoster("Bağlantı Hatası", "Eren'in sunucusuna ulaşılamıyor.", false);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Kütüphane', style: TextStyle(fontWeight: FontWeight.bold, color: Colors.amberAccent)),
        actions: [
          Padding(
            padding: const EdgeInsets.only(right: 16.0),
            child: Center(
              child: Text(
                '1,250000 Jeton', // İleride API'den gelecek
                style: TextStyle(color: Colors.amber.shade300, fontWeight: FontWeight.bold, fontSize: 16),
              ),
            ),
          )
        ],
      ),
      body: _yukleniyor
          ? const Center(child: CircularProgressIndicator(color: Colors.amberAccent))
          : _kitaplarRafiniOlustur(),

      // En alttaki Menü (Arama, Cüzdan vs.)
      bottomNavigationBar: BottomNavigationBar(
        backgroundColor: const Color(0xFF1E1E1E),
        selectedItemColor: Colors.amberAccent,
        unselectedItemColor: Colors.grey,
        items: const [
          BottomNavigationBarItem(icon: Icon(Icons.library_books), label: 'Kütüphane'),
          BottomNavigationBarItem(icon: Icon(Icons.search), label: 'Arama'),
          BottomNavigationBarItem(icon: Icon(Icons.account_balance_wallet), label: 'Cüzdan'),
        ],
      ),
    );
  }

  // Aşağı doğru kaydırılan, rafta duran kitaplar ızgarası
  Widget _kitaplarRafiniOlustur() {
    // Kitapları ikili gruplar (raflar) halinde ayırıyoruz
    List<List<dynamic>> raflar = [];
    for (var i = 0; i < _kitaplar.length; i += 2) {
      raflar.add([
        _kitaplar[i],
        if (i + 1 < _kitaplar.length) _kitaplar[i + 1]
      ]);
    }

    return ListView.builder(
      padding: const EdgeInsets.only(top: 20, bottom: 40),
      itemCount: raflar.isEmpty ? 2 : raflar.length,
      itemBuilder: (context, index) {
        // API boşsa test verisi kullan
        final rafKitaplari = raflar.isNotEmpty
            ? raflar[index]
            : [{"title": "Yükleniyor...", "isLocked": true}, {"title": "Bekleniyor...", "isLocked": true}];

        return Padding(
          padding: const EdgeInsets.symmetric(horizontal: 20.0),
          child: Column(
            children: [
              // Kitapların kendisi (Rafa sıfır oturacak şekilde ayarlandı)
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                crossAxisAlignment: CrossAxisAlignment.end, // Kitapları rafın zeminine hizalar
                children: rafKitaplari.map((kitap) => Expanded(
                  child: Padding(
                    padding: const EdgeInsets.symmetric(horizontal: 10.0),
                    child: AspectRatio(
                      aspectRatio: 0.7, // Kitap boyut oranı (uzun ince)
                      child: _kitapKarti(kitap),
                    ),
                  ),
                )).toList(),
              ),
              // Fiziksel Raf Çizgisi (3D Kabartma ve Gölge)
              Container(
                height: 12,
                margin: const EdgeInsets.only(bottom: 40), // Bir sonraki raf ile aradaki boşluk
                decoration: BoxDecoration(
                  color: const Color(0xFF2A2A2A), // Rafın kendi rengi
                  borderRadius: BorderRadius.circular(4),
                  border: const Border(
                    top: BorderSide(color: Colors.white24, width: 1), // Üstten ışık vurma hissi
                  ),
                  boxShadow: [
                    BoxShadow(
                      color: Colors.black.withValues(alpha: 0.8), // Rafın altındaki ağır gölge
                      blurRadius: 15,
                      offset: const Offset(0, 10),
                    )
                  ],
                ),
              )
            ],
          ),
        );
      },
    );
  }

  // Tek bir kitabın tasarımı (Kapak ve kilit ikonu)
  Widget _kitapKarti(dynamic kitap) {
    bool kilitliMi = kitap['isLocked'] ?? true;

    return GestureDetector(
      onTap: () {
        _kitapDetayPenceresiniAc(kitap); // Tıklanınca alt pencereyi aç
      },
      child: Container(
        decoration: BoxDecoration(
          color: const Color(0xFF2C4A52),
          borderRadius: BorderRadius.circular(8),
          boxShadow: [
            // withOpacity(0.5) yerine withValues(alpha: 0.5) kullanıyoruz
            BoxShadow(color: Colors.black.withValues(alpha: 0.5), blurRadius: 8, offset: const Offset(4, 4))
          ],
          // withOpacity(0.3) yerine withValues(alpha: 0.3)
          border: Border.all(color: Colors.amberAccent.withValues(alpha: 0.3), width: 1),
        ),
        child: Stack(
          children: [
            Center(
              child: Padding(
                padding: const EdgeInsets.all(12.0),
                child: Text(
                  kitap['title'] ?? 'Bilinmeyen Kitap',
                  textAlign: TextAlign.center,
                  style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 18, color: Colors.white70),
                ),
              ),
            ),
            if (kilitliMi)
              Positioned(
                bottom: 8,
                right: 8,
                child: Container(
                  padding: const EdgeInsets.all(6),
                  decoration: BoxDecoration(
                    // withOpacity(0.6) yerine withValues(alpha: 0.6)
                    color: Colors.black.withValues(alpha: 0.6),
                    shape: BoxShape.circle,
                  ),
                  child: const Icon(Icons.lock, color: Colors.amberAccent, size: 20),
                ),
              ),
          ],
        ),
      ),
    );
  }
  // TAM EKRANA YAKIN ŞIK DETAY PENCERESİ
  void _kitapDetayPenceresiniAc(dynamic kitap) {
    bool kilitliMi = kitap['isLocked'] ?? true;

    // API'den henüz gelmeyen veriler için şık duracak geçici (dummy) veriler
    String sayfaSayisi = kitap['pages']?.toString() ?? "120";
    String sure = kitap['duration']?.toString() ?? "15 Dk";
    String tur = kitap['category'] ?? "Genel Kültür";
    String yazar = kitap['author'] ?? "Anonim";
    String aciklama = kitap['content'] ??
        "Bu kitabın derinliklerinde okyanusun en karanlık sırlarını keşfedecek, daha önce hiç duymadığınız efsanelere tanık olacaksınız. Her sayfasında yeni bir gizem, her bölümünde yepyeni bir macera sizi bekliyor. Bilgeliğe giden bu yolda kilitleri kırmaya hazır mısınız?";

    showModalBottomSheet(
      context: context,
      backgroundColor: Colors.transparent,
      isScrollControlled: true, // Pencerenin tam ekrana kadar açılabilmesine izin verir
      builder: (context) {
        return Container(
          // Ekranın %85'ini kaplayan devasa, ferah bir alan
          height: MediaQuery.of(context).size.height * 0.85,
          decoration: const BoxDecoration(
            color: Color(0xFF15151C), // Çok koyu, premium lacivert/siyah arka plan
            borderRadius: BorderRadius.only(
              topLeft: Radius.circular(32),
              topRight: Radius.circular(32),
            ),
          ),
          child: Column(
            children: [
              // Üstteki küçük çekme çizgisi
              const SizedBox(height: 12),
              Container(
                width: 50,
                height: 5,
                decoration: BoxDecoration(color: Colors.grey.shade700, borderRadius: BorderRadius.circular(10)),
              ),
              const SizedBox(height: 20),

              // ORTA KISIM: Kaydırılabilir İçerik (Uzun yazılar için)
              Expanded(
                child: SingleChildScrollView(
                  padding: const EdgeInsets.symmetric(horizontal: 24),
                  child: Column(
                    children: [
                      // Dev Kitap Kapağı
                      Container(
                        height: 250,
                        decoration: BoxDecoration(
                            boxShadow: [
                              BoxShadow(color: Colors.black.withValues(alpha: 0.6), blurRadius: 20, offset: const Offset(0, 10))
                            ]
                        ),
                        child: AspectRatio(
                          aspectRatio: 0.7,
                          child: _kitapKarti(kitap), // Vitrindeki aynı kapağı çağırıyoruz
                        ),
                      ),
                      const SizedBox(height: 24),

                      // Kitap Adı ve Yazar
                      Text(
                        kitap['title'] ?? 'Bilinmeyen Kitap',
                        style: const TextStyle(fontSize: 26, fontWeight: FontWeight.bold, color: Colors.white),
                        textAlign: TextAlign.center,
                      ),
                      const SizedBox(height: 8),
                      Text(
                        yazar,
                        style: TextStyle(fontSize: 16, color: Colors.grey.shade400, fontStyle: FontStyle.italic),
                      ),
                      const SizedBox(height: 24),

                      // İSTATİSTİKLER (Şık bir kutu içinde ikonlu gösterim)
                      Container(
                        padding: const EdgeInsets.symmetric(vertical: 16),
                        decoration: BoxDecoration(
                          color: Colors.white.withValues(alpha: 0.05),
                          borderRadius: BorderRadius.circular(16),
                        ),
                        child: Row(
                          mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                          children: [
                            _infoSutunu(Icons.category, "Tür", tur),
                            _infoSutunu(Icons.timer, "Süre", sure),
                            _infoSutunu(Icons.menu_book, "Sayfa", sayfaSayisi),
                          ],
                        ),
                      ),
                      const SizedBox(height: 24),

                      // Kitabın Tam Konusu / Özeti
                      const Align(
                        alignment: Alignment.centerLeft,
                        child: Text(
                          "Konusu",
                          style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold, color: Colors.white),
                        ),
                      ),
                      const SizedBox(height: 12),
                      Text(
                        aciklama,
                        style: const TextStyle(fontSize: 15, color: Colors.white70, height: 1.6),
                        textAlign: TextAlign.justify, // Metni iki yana yaslar, kitap gibi durur
                      ),
                      const SizedBox(height: 30), // Kaydırma payı
                    ],
                  ),
                ),
              ),

              // ALT KISIM: Sabit Aksiyon Butonları (Kaydırmadan Etkilenmez)
              Container(
                padding: const EdgeInsets.fromLTRB(24, 16, 24, 32), // Alt kısımdan iPhone çentiği için boşluk
                decoration: BoxDecoration(
                    color: const Color(0xFF15151C),
                    boxShadow: [
                      BoxShadow(color: Colors.black.withValues(alpha: 0.5), blurRadius: 10, offset: const Offset(0, -5))
                    ]
                ),
                child: Row(
                  children: [
                    // Favorilere Ekle Butonu
                    Container(
                      decoration: BoxDecoration(border: Border.all(color: Colors.grey.shade800), shape: BoxShape.circle),
                      child: IconButton(icon: const Icon(Icons.favorite_border, color: Colors.white), onPressed: () {}),
                    ),
                    const SizedBox(width: 12),

                    // Paylaş Butonu
                    Container(
                      decoration: BoxDecoration(border: Border.all(color: Colors.grey.shade800), shape: BoxShape.circle),
                      child: IconButton(icon: const Icon(Icons.ios_share, color: Colors.white), onPressed: () {}),
                    ),
                    const SizedBox(width: 16),

                    // Ana Aksiyon Butonu (Kilit Aç / Okumaya Başla)
                    Expanded(
                      child: SizedBox(
                        height: 55,
                        child: ElevatedButton(
                          style: ElevatedButton.styleFrom(
                            backgroundColor: kilitliMi ? const Color(0xFF2C4A52) : Colors.amber.shade700,
                            shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
                            elevation: 0,
                          ),
                          // AKSİYON BUTONU MANTIĞI
                          onPressed: () {
                            if (kilitliMi) {
                              _kilitAc(kitap['id']);
                            } else {
                              Navigator.pop(context); // Alt pencereyi kapatır
                              Navigator.push(          // Okuma ekranına yönlendirir
                                context,
                                MaterialPageRoute(
                                  builder: (context) => OkumaEkrani(kitap: kitap),
                                ),
                              );
                            }
                          },
                          child: Text(
                            kilitliMi ? "100 JETONLA KİLİDİ AÇ" : "OKUMAYA BAŞLA",
                            style: const TextStyle(fontSize: 15, fontWeight: FontWeight.bold, color: Colors.white),
                          ),
                        ),
                      ),
                    ),
                  ],
                ),
              ),
            ],
          ),
        );
      },
    );
  }

  // İstatistikleri (Tür, Süre, Sayfa) düzenli göstermek için yardımcı parçacık
  Widget _infoSutunu(IconData ikon, String baslik, String deger) {
    return Column(
      children: [
        Icon(ikon, color: Colors.amber.shade300, size: 24),
        const SizedBox(height: 8),
        Text(deger, style: const TextStyle(color: Colors.white, fontWeight: FontWeight.bold, fontSize: 14)),
        const SizedBox(height: 4),
        Text(baslik, style: TextStyle(color: Colors.grey.shade500, fontSize: 12)),
      ],
    );
  }
  // Ekrana şık bir uyarı kutusu çıkaran fonksiyon
  void _mesajGoster(String baslik, String icerik, bool basariliMi) {
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        backgroundColor: const Color(0xFF1E1E26),
        shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(20)),
        title: Text(baslik, style: TextStyle(color: basariliMi ? Colors.green : Colors.redAccent)),
        content: Text(icerik, style: const TextStyle(color: Colors.white70)),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context),
            child: const Text("Tamam", style: TextStyle(color: Colors.amberAccent)),
          ),
        ],
      ),
    );
  }
}
// OKUMA EKRANI ARAYÜZÜ
class OkumaEkrani extends StatelessWidget {
  final dynamic kitap;

  const OkumaEkrani({super.key, required this.kitap});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color(0xFF121212),
      appBar: AppBar(
        title: Text(kitap['title'] ?? '', style: const TextStyle(color: Colors.amberAccent, fontSize: 18)),
        backgroundColor: const Color(0xFF1E1E1E),
        elevation: 0,
        iconTheme: const IconThemeData(color: Colors.amberAccent),
      ),
      body: SingleChildScrollView(
        padding: const EdgeInsets.symmetric(horizontal: 24.0, vertical: 32.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // KİTAP BAŞLIĞI
            Text(
              kitap['title'] ?? 'Bilinmeyen Kitap',
              style: const TextStyle(
                fontSize: 28,
                fontWeight: FontWeight.bold,
                color: Colors.white,
                letterSpacing: 1.2,
              ),
            ),
            const SizedBox(height: 16),

            // YAZAR VE KATEGORİ BİLGİSİ
            Text(
              "${kitap['author'] ?? 'Anonim'}  •  ${kitap['category'] ?? 'Genel Kültür'}",
              style: TextStyle(
                fontSize: 14,
                color: Colors.amber.shade300,
                fontStyle: FontStyle.italic,
              ),
            ),
            const SizedBox(height: 40),

            // KİTAP İÇERİĞİ
            Text(
              kitap['content'] ?? 'Bu kitabın içeriği henüz yüklenmedi.',
              style: const TextStyle(
                fontSize: 18,
                color: Colors.white70,
                height: 1.8,
              ),
              textAlign: TextAlign.justify,
            ),
            const SizedBox(height: 60),

            // TEST VE ÖDÜL GEÇİŞ BUTONU
            SizedBox(
              width: double.infinity,
              height: 55,
              child: ElevatedButton(
                style: ElevatedButton.styleFrom(
                  backgroundColor: const Color(0xFF2C4A52),
                  shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
                  elevation: 5,
                ),
                onPressed: () {
                  _mesajGoster(context, "Soru Ekranı", "Kitap bitirildi, test ekranına geçiş yapılacak.");
                },
                child: const Text(
                  'KİTABI BİTİR VE SORULARA GEÇ',
                  style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold, color: Colors.white),
                ),
              ),
            ),
            const SizedBox(height: 40),
          ],
        ),
      ),
    );
  }

  // YARDIMCI BİLDİRİM FONKSİYONU
  void _mesajGoster(BuildContext context, String baslik, String icerik) {
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        backgroundColor: const Color(0xFF1E1E26),
        title: Text(baslik, style: const TextStyle(color: Colors.amberAccent)),
        content: Text(icerik, style: const TextStyle(color: Colors.white70)),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context),
            child: const Text("Tamam", style: TextStyle(color: Colors.white)),
          ),
        ],
      ),
    );
  }
}