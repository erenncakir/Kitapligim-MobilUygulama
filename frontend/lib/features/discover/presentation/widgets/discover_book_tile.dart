import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

import '../../../../core/network/book_model.dart';
import '../../../../core/theme/app_colors.dart';
import '../book_detail_page.dart';
import '../../../library/models/book.dart';
import '../../../library/presentation/widgets/book_card.dart';

/// Keşfet grid hücresi: açık kitap veya kilit + jeton overlay’i.
class DiscoverBookTile extends StatelessWidget {
  const DiscoverBookTile({
    super.key,
    required this.book,
  });

  final BookModel book;

  static const double _cardRadius = 8;
  static const _coverPalette = <Color>[
    Color(0xFF3B82F6),
    Color(0xFF10B981),
    Color(0xFFF97316),
    Color(0xFF8B5CF6),
    Color(0xFFEF4444),
    Color(0xFF14B8A6),
  ];

  @override
  Widget build(BuildContext context) {
    final uiBook = Book(
      title: book.title,
      coverColor: _coverPalette[book.id % _coverPalette.length],
    );

    return Stack(
      fit: StackFit.expand,
      children: [
        BookCard(
          book: uiBook,
          onTap: () => _openBook(context, book),
        ),
        if (book.isLocked)
          Positioned.fill(
            child: IgnorePointer(
              child: ClipRRect(
                borderRadius: BorderRadius.circular(_cardRadius),
                child: ColoredBox(
                  color: Colors.black.withValues(alpha: 0.6),
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      const Icon(
                        Icons.lock_rounded,
                        color: AppColors.gold,
                        size: 40,
                      ),
                      const SizedBox(height: 10),
                      Text(
                        '${book.unlockCost} 🪙',
                        textAlign: TextAlign.center,
                        style: GoogleFonts.nunito(
                          color: AppColors.gold,
                          fontWeight: FontWeight.w800,
                          fontSize: 16,
                        ),
                      ),
                    ],
                  ),
                ),
              ),
            ),
          ),
      ],
    );
  }

  void _openBook(BuildContext context, BookModel book) {
    Navigator.of(context).push(
      MaterialPageRoute<void>(
        builder: (_) => BookDetailPage(book: book),
      ),
    );
  }
}
