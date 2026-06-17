import 'package:flutter/material.dart';

import '../../../core/network/api_service.dart';
import '../../../core/network/book_model.dart';
import '../../../core/network/user_model.dart';
import '../../../core/state/token_notifier.dart';
import '../../reading/presentation/book_reading_page.dart';

class BookDetailPage extends StatefulWidget {
  const BookDetailPage({super.key, required this.book});

  final BookModel book;

  @override
  State<BookDetailPage> createState() => _BookDetailPageState();
}

class _BookDetailPageState extends State<BookDetailPage> {
  static const String _deviceId = 'test-device-1';
  bool _isUnlocking = false;
  bool _isUnlocked = false;

  static const _coverPalette = <Color>[
    Color(0xFF3B82F6),
    Color(0xFF10B981),
    Color(0xFFF97316),
    Color(0xFF8B5CF6),
    Color(0xFFEF4444),
    Color(0xFF14B8A6),
  ];

  Future<void> _showUnlockDialog() async {
    final confirmed = await showDialog<bool>(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Kitabi Aç'),
        content: Text(
          '${widget.book.unlockCost} Jeton harcanacak. Emin misiniz?',
        ),
        actions: [
          TextButton(
            onPressed: () => Navigator.of(context).pop(false),
            child: const Text('İptal'),
          ),
          TextButton(
            onPressed: () => Navigator.of(context).pop(true),
            child: const Text('Onayla'),
          ),
        ],
      ),
    );

    if (confirmed == true) {
      if (!context.mounted) return;
      await _unlockBook();
    }
  }

  Future<void> _unlockBook() async {
    setState(() => _isUnlocking = true);
    final messenger = ScaffoldMessenger.of(context);
    final navigator = Navigator.of(context);

    try {
      final success = await ApiService().unlockBook(_deviceId, widget.book.id);

      if (!context.mounted) return;

      setState(() => _isUnlocking = false);

      if (success) {
        setState(() => _isUnlocked = true);
        final UserModel user = await ApiService().getUser(_deviceId);
        if (!context.mounted) return;
        tokenBalanceNotifier.value = user.totalPoints;
        messenger.showSnackBar(const SnackBar(content: Text('Başarili')));
        navigator.push(
          MaterialPageRoute<void>(
            builder: (_) => BookReadingPage(book: widget.book),
          ),
        );
      } else {
        messenger.showSnackBar(const SnackBar(content: Text('İşlem başarisiz.')));
      }
    } catch (e) {
      if (!context.mounted) return;
      messenger.showSnackBar(
        SnackBar(
          content: Text(e.toString().replaceAll('Exception: ', '')),
          backgroundColor: Colors.redAccent,
          behavior: SnackBarBehavior.floating,
        ),
      );
    }
  }

  void _openReadingPage() {
    Navigator.of(context).push(
      MaterialPageRoute<void>(
        builder: (_) => BookReadingPage(book: widget.book),
      ),
    );
  }

  void _onBottomButtonPressed() {
    final isLocked = widget.book.isLocked && !_isUnlocked;
    if (isLocked) {
      _showUnlockDialog();
    } else {
      _openReadingPage();
    }
  }

  @override
  Widget build(BuildContext context) {
    final book = widget.book;
    final coverColor = _coverPalette[widget.book.id % _coverPalette.length];
    final isLocked = widget.book.isLocked && !_isUnlocked;

    return Scaffold(
      extendBodyBehindAppBar: true,
      appBar: AppBar(backgroundColor: Colors.transparent, elevation: 0),
      bottomNavigationBar: SafeArea(
        minimum: const EdgeInsets.fromLTRB(16, 10, 16, 16),
        child: SizedBox(
          height: 54,
          child: ElevatedButton(
            onPressed: _isUnlocking ? null : _onBottomButtonPressed,
            style: ElevatedButton.styleFrom(
              backgroundColor: isLocked
                  ? const Color(0xFF1F2937)
                  : const Color(0xFFFACC15),
              foregroundColor: isLocked
                  ? const Color(0xFFFACC15)
                  : Colors.black,
              disabledBackgroundColor: const Color(0xFF1F2937),
              disabledForegroundColor: const Color(0xFFFACC15),
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(14),
              ),
            ),
            child: _isUnlocking
                ? const SizedBox(
                    width: 20,
                    height: 20,
                    child: CircularProgressIndicator(
                      strokeWidth: 2,
                      color: Color(0xFFFACC15),
                    ),
                  )
                : Text(
                    isLocked
                        ? '🔒 ${widget.book.unlockCost} Jeton ile Kilidi Aç'
                        : '📖 Okumaya Başla',
                    style: const TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w800,
                    ),
                  ),
          ),
        ),
      ),
      body: SingleChildScrollView(
        padding: const EdgeInsets.fromLTRB(16, kToolbarHeight + 36, 16, 24),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            AspectRatio(
              aspectRatio: 0.72,
              child: ClipRRect(
                borderRadius: BorderRadius.circular(16),
                child: Container(
                  color: coverColor,
                  alignment: Alignment.center,
                  padding: const EdgeInsets.all(18),
                  child: Text(
                    widget.book.title,
                    textAlign: TextAlign.center,
                    style: const TextStyle(
                      color: Colors.white,
                      fontSize: 30,
                      fontWeight: FontWeight.w800,
                    ),
                  ),
                ),
              ),
            ),
            const SizedBox(height: 20),
            Text(
              widget.book.title,
              style: const TextStyle(fontSize: 28, fontWeight: FontWeight.w800),
            ),
            const SizedBox(height: 10),
            Wrap(
              spacing: 14,
              runSpacing: 8,
              children: [
                const Text(
                  '⭐ 4.8',
                  style: TextStyle(fontSize: 15, fontWeight: FontWeight.w700),
                ),
                const Text(
                  '📄 120 Sayfa',
                  style: TextStyle(fontSize: 15, fontWeight: FontWeight.w700),
                ),
                Text(
                  '🏷️ ${widget.book.category}',
                  style: const TextStyle(
                    fontSize: 15,
                    fontWeight: FontWeight.w700,
                  ),
                ),
              ],
            ),
            const SizedBox(height: 22),
            const Text(
              'Hakkında',
              style: TextStyle(fontSize: 20, fontWeight: FontWeight.w800),
            ),
            const SizedBox(height: 8),
            Text(
              book.description ??
                  'Bu kitap için henüz bir açıklama girilmemiş.',
              style: const TextStyle(color: Colors.white70, height: 1.5),
            ),
            const SizedBox(height: 90),
          ],
        ),
      ),
    );
  }
}
