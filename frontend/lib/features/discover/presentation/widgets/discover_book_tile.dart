import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

import '../../../../core/network/book_model.dart';
import '../../../../core/theme/app_colors.dart';
import '../book_detail_page.dart';
import '../../../library/models/book.dart';
import '../../../library/presentation/widgets/book_card.dart';

/// Keşfet grid hücresi: açık kitap veya kilit + jeton overlay'i.
class DiscoverBookTile extends StatelessWidget {
  const DiscoverBookTile({
    super.key,
    required this.book,
    this.onReturn,
  });

  final BookModel book;
  final VoidCallback? onReturn;

  static const double _cardRadius = 8;

  @override
  Widget build(BuildContext context) {
    final uiBook = Book(
      title: book.title,
      imageUrl: book.imageUrl,
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

  Future<void> _openBook(BuildContext context, BookModel book) async {
    await Navigator.of(context).push(
      MaterialPageRoute<void>(
        builder: (_) => BookDetailPage(book: book),
      ),
    );
    onReturn?.call();
  }
}
