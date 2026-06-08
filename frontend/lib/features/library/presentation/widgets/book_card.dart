import 'package:flutter/material.dart';
import '../../models/book.dart';

String getDirectDriveLink(String url) {
  if (url.contains('drive.google.com/file/d/')) {
    final RegExp regExp = RegExp(r'/file/d/([a-zA-Z0-9_-]+)');
    final match = regExp.firstMatch(url);
    if (match != null && match.groupCount >= 1) {
      final fileId = match.group(1);
      return 'https://drive.google.com/uc?export=download&id=$fileId';
    }
  }
  return url;
}

class BookCoverImage extends StatelessWidget {
  const BookCoverImage({
    super.key,
    required this.imageUrl,
    this.fit = BoxFit.cover,
    this.borderRadius,
  });

  final String? imageUrl;
  final BoxFit fit;
  final BorderRadius? borderRadius;

  @override
  Widget build(BuildContext context) {
    final url = imageUrl?.trim();
    final child = url != null && url.isNotEmpty
        ? Image.network(
            getDirectDriveLink(url),
            fit: fit,
            loadingBuilder: (context, child, loadingProgress) {
              if (loadingProgress == null) return child;
              return const Center(
                child: CircularProgressIndicator(strokeWidth: 2),
              );
            },
            errorBuilder: (_, _, _) => const _CoverPlaceholder(),
          )
        : const _CoverPlaceholder();

    if (borderRadius == null) return child;

    return ClipRRect(borderRadius: borderRadius!, child: child);
  }
}

class _CoverPlaceholder extends StatelessWidget {
  const _CoverPlaceholder();

  @override
  Widget build(BuildContext context) {
    return const ColoredBox(
      color: Color(0xFF374151),
      child: Center(
        child: Icon(Icons.book_rounded, color: Colors.white54, size: 40),
      ),
    );
  }
}

class BookCard extends StatelessWidget {
  const BookCard({super.key, required this.book, this.onTap});

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
              child: Stack(
                fit: StackFit.expand,
                children: [
                  BookCoverImage(imageUrl: book.imageUrl, fit: BoxFit.cover),
                  Row(
                    children: [
                      Container(
                        width: _spineWidth,
                        color: Colors.black.withValues(alpha: 0.2),
                      ),
                    ],
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
