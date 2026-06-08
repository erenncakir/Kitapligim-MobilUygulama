import 'package:flutter/material.dart';

import '../../../core/network/api_service.dart';
import '../../../core/network/user_model.dart';
import '../../../core/state/optimistic_unlock_notifier.dart';
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
  int _unlockedBookCount = 0;
  Set<int> _knownUnlockedIds = {};
  static const String _deviceId = 'test-device-2';

  static const List<Widget> _pages = [
    MyLibraryPage(),
    DiscoverPage(),
    WalletPage(),
  ];

  @override
  void initState() {
    super.initState();
    tokenBalanceNotifier.addListener(_onTokenBalanceChanged);
    optimisticallyUnlockedBookIds.addListener(_onOptimisticUnlock);
    _loadData();
  }

  @override
  void dispose() {
    tokenBalanceNotifier.removeListener(_onTokenBalanceChanged);
    optimisticallyUnlockedBookIds.removeListener(_onOptimisticUnlock);
    super.dispose();
  }

  void _onTokenBalanceChanged() {
    if (mounted) setState(() {});
  }

  void _onOptimisticUnlock() {
    if (!mounted) return;
    setState(() {
      _unlockedBookCount = {
        ..._knownUnlockedIds,
        ...optimisticallyUnlockedBookIds.value,
      }.length;
    });
    _loadUnlockedBookCount();
  }

  Future<void> _loadData() async {
    try {
      final UserModel user = await ApiService().getUser(_deviceId);
      tokenBalanceNotifier.value = user.totalPoints;
      if (mounted) {
        setState(() => _user = user);
      }
    } catch (_) {}
    await _loadUnlockedBookCount();
  }

  Future<void> _loadUnlockedBookCount() async {
    try {
      final unlockedIds = await ApiService().getUnlockedBookIds(_deviceId);
      _knownUnlockedIds = unlockedIds.toSet();
      final mergedCount = {
        ..._knownUnlockedIds,
        ...optimisticallyUnlockedBookIds.value,
      }.length;
      if (mounted) {
        setState(() => _unlockedBookCount = mergedCount);
      }
    } catch (_) {}
  }

  @override
  Widget build(BuildContext context) {
    final tokenBalance = tokenBalanceNotifier.value;
    final userId = _user?.id ?? _deviceId;

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
                            userId,
                            style: const TextStyle(
                              fontSize: 18,
                              fontWeight: FontWeight.w700,
                            ),
                            maxLines: 1,
                            overflow: TextOverflow.ellipsis,
                          ),
                          const SizedBox(height: 4),
                          Text(
                            'Jeton: $tokenBalance',
                            style: const TextStyle(
                              color: Colors.black54,
                              fontWeight: FontWeight.w600,
                            ),
                          ),
                        ],
                      ),
                    ),
                  ],
                ),
              ),
              ListTile(
                leading: const Text('🪙', style: TextStyle(fontSize: 18)),
                title: Text('Toplam Jeton: $tokenBalance'),
              ),
              ListTile(
                leading: const Text('📚', style: TextStyle(fontSize: 18)),
                title: Text('Açılan Kitaplar: $_unlockedBookCount'),
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
      body: IndexedStack(index: _index, children: _pages),
      bottomNavigationBar: ClipRRect(
        borderRadius: const BorderRadius.vertical(top: Radius.circular(16)),
        child: BottomNavigationBar(
          currentIndex: _index,
          onTap: (i) {
            setState(() => _index = i);
            if (i == 0) {
              _loadUnlockedBookCount();
            }
          },
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
