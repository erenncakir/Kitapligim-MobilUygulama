import 'package:flutter/material.dart';

import '../../../core/network/api_service.dart';
import '../../../core/network/book_model.dart';
import '../../../core/network/user_model.dart';
import '../../../core/state/token_notifier.dart';
import '../../../core/theme/app_colors.dart';
import '../../discover/presentation/discover_page.dart';
import '../../library/presentation/my_library_page.dart';
import '../../wallet/presentation/wallet_page.dart';
import 'widgets/token_balance_app_bar.dart';

class MainShellPage extends StatefulWidget {
  const MainShellPage({super.key});

  @override
  State<MainShellPage> createState() => _MainShellPageState();
}

class _MainShellPageState extends State<MainShellPage> {
  int _index = 0;
  UserModel? _user;
  List<BookModel> _books = [];
  static const String _deviceId = 'test-device-1';

  static const List<Widget> _pages = [
    MyLibraryPage(),
    DiscoverPage(),
    WalletPage(),
  ];

  @override
  void initState() {
    super.initState();
    _loadData();
  }

  Future<void> _loadData() async {
    try {
      final UserModel user = await ApiService().getUser(_deviceId);
      tokenBalanceNotifier.value = user.totalPoints;
      if (mounted) {
        setState(() => _user = user);
      }
    } catch (_) {}
    try {
      final books = await ApiService().getBooks();
      if (mounted) {
        setState(() => _books = books);
      }
    } catch (_) {}
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: const TokenBalanceAppBar(),
      drawer: Drawer(
        child: SafeArea(
          child: Column(
            children: [
              DrawerHeader(
                margin: EdgeInsets.zero,
                child: Row(
                  children: [
                    const CircleAvatar(
                      radius: 34,
                      backgroundColor: AppColors.woodShelf,
                      child: Icon(
                        Icons.person_rounded,
                        size: 36,
                        color: Colors.white,
                      ),
                    ),
                    const SizedBox(width: 14),
                    Expanded(
                      child: Column(
                        mainAxisAlignment: MainAxisAlignment.center,
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            'Misafir Kullanıcı',
                            style: TextStyle(
                              fontSize: 18,
                              fontWeight: FontWeight.w700,
                            ),
                          ),
                          SizedBox(height: 4),
                          Text(
                            'ID: ${_user?.id ?? 'Yükleniyor...'}',
                            style: TextStyle(color: Colors.black54),
                          ),
                        ],
                      ),
                    ),
                  ],
                ),
              ),
              ListTile(
                leading: const Text('📚', style: TextStyle(fontSize: 18)),
                title: Text(
                  'Kitaplığındaki Kitaplar: ${_books.where((b) => !b.isLocked).length}',
                ),
              ),
              ListTile(
                leading: const Text('🧠', style: TextStyle(fontSize: 18)),
                title: Text(
                  'Çözülen Sorular: ${(_user?.totalPoints ?? 0) ~/ 10}',
                ),
              ),
              const Spacer(),
              ListTile(
                leading: const Text('🚪', style: TextStyle(fontSize: 18)),
                title: const Text(
                  'Çıkış Yap',
                  style: TextStyle(
                    color: Colors.red,
                    fontWeight: FontWeight.w700,
                  ),
                ),
                onTap: () {
                  Navigator.of(context).pop();
                },
              ),
              const SizedBox(height: 12),
            ],
          ),
        ),
      ),
      body: IndexedStack(
        index: _index,
        children: _pages,
      ),
      bottomNavigationBar: ClipRRect(
        borderRadius: const BorderRadius.vertical(
          top: Radius.circular(16),
        ),
        child: BottomNavigationBar(
          currentIndex: _index,
          onTap: (i) => setState(() => _index = i),
          backgroundColor: AppColors.woodShelf,
          selectedItemColor: AppColors.gold,
          unselectedItemColor: Colors.white60,
          type: BottomNavigationBarType.fixed,
          elevation: 12,
          items: const [
            BottomNavigationBarItem(
              icon: Icon(Icons.menu_book_rounded),
              label: 'Kitaplığım',
            ),
            BottomNavigationBarItem(
              icon: Icon(Icons.explore_rounded),
              label: 'Kütüphane',
            ),
            BottomNavigationBarItem(
              icon: Icon(Icons.account_balance_wallet_rounded),
              label: 'Cüzdan',
            ),
          ],
        ),
      ),
    );
  }
}
