import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

import '../../models/book.dart';

class BookCard extends StatelessWidget {
  const BookCard({
    super.key,
    required this.book,
    this.onTap,
  });

  final Book book;
  final VoidCallback? onTap;

  static const double aspectRatio = 100 / 150;

  static const double _spineWidth = 12;
  static const BorderRadius _radius = BorderRadius.horizontal(
    right: Radius.circular(8),
  );

  @override
  Widget build(BuildContext context) {
    return Material(
      color: Colors.transparent,
      child: InkWell(
        borderRadius: _radius,
        onTap: onTap,
        child: AspectRatio(
          aspectRatio: aspectRatio,
          child: Container(
            decoration: BoxDecoration(
              color: book.coverColor,
              borderRadius: _radius,
              boxShadow: [
                BoxShadow(
                  color: Colors.black.withValues(alpha: 0.35),
                  blurRadius: 10,
                  offset: const Offset(4, 6),
                ),
              ],
            ),
            child: ClipRRect(
              borderRadius: _radius,
              child: Row(
                children: [
                  Container(
                    width: _spineWidth,
                    color: Colors.black.withValues(alpha: 0.2),
                  ),
                  Expanded(
                    child: Padding(
                      padding: const EdgeInsets.symmetric(
                        horizontal: 8,
                        vertical: 10,
                      ),
                      child: Center(
                        child: Text(
                          book.title,
                          textAlign: TextAlign.center,
                          maxLines: 5,
                          overflow: TextOverflow.ellipsis,
                          style: GoogleFonts.nunito(
                            color: Colors.white,
                            fontWeight: FontWeight.w800,
                            fontSize: 15,
                            height: 1.15,
                            shadows: const [
                              Shadow(
                                offset: Offset(0, 1),
                                blurRadius: 3,
                                color: Color(0x66000000),
                              ),
                            ],
                          ),
                        ),
                      ),
                    ),
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}
