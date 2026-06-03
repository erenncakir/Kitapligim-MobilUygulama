import 'package:flutter/material.dart';

import '../../../core/network/api_service.dart';
import '../../../core/network/book_model.dart';
import '../../discover/presentation/book_detail_page.dart';
import '../models/book.dart';
import 'widgets/book_card.dart';

class MyLibraryPage extends StatelessWidget {
  const MyLibraryPage({super.key});

  static const double _horizontalPad = 16;
  static const double _columnGap = 20;
  static const double _gapBetweenShelves = 30;
  static const double _childAspectRatio = 0.65;
  static const int _booksPerRow = 2;
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
    return ColoredBox(
      color: Theme.of(context).scaffoldBackgroundColor,
      child: FutureBuilder<List<BookModel>>(
        future: ApiService().getBooks(),
        builder: (context, snapshot) {
          if (snapshot.connectionState != ConnectionState.done) {
            return const Center(child: CircularProgressIndicator());
          }
          if (snapshot.hasError) {
            return Center(
              child: Text(
                'Kitaplar yüklenemedi',
                style: Theme.of(context).textTheme.titleMedium,
              ),
            );
          }

          final books =
              (snapshot.data ?? const <BookModel>[])
                  .where((book) => !book.isLocked)
                  .toList();
          final rowCount = (books.length + _booksPerRow - 1) ~/ _booksPerRow;

          return CustomScrollView(
            physics: const BouncingScrollPhysics(
              parent: AlwaysScrollableScrollPhysics(),
            ),
            slivers: [
              SliverToBoxAdapter(
                child: Column(
                  children: [
                    const SizedBox(height: 20),
                    Center(
                      child: Container(
                        width: 200,
                        height: 45,
                        decoration: BoxDecoration(
                          color: const Color(0xFF6B4226),
                          borderRadius: BorderRadius.circular(15),
                          border: Border.all(
                            color: const Color(0xFF3E2312),
                            width: 3,
                          ),
                          boxShadow: [
                            BoxShadow(
                              color: Colors.black.withValues(alpha: 0.25),
                              blurRadius: 8,
                              offset: const Offset(0, 4),
                            ),
                          ],
                        ),
                        alignment: Alignment.center,
                        child: const Text(
                          'Kitaplığım',
                          textAlign: TextAlign.center,
                          style: TextStyle(
                            color: Colors.white,
                            fontWeight: FontWeight.bold,
                            fontSize: 20,
                          ),
                        ),
                      ),
                    ),
                    const SizedBox(height: 20),
                  ],
                ),
              ),
              SliverPadding(
                padding: const EdgeInsets.fromLTRB(
                  _horizontalPad,
                  0,
                  _horizontalPad,
                  24,
                ),
                sliver: SliverList(
                  delegate: SliverChildBuilderDelegate(
                    (context, rowIndex) {
                      final startIndex = rowIndex * _booksPerRow;
                      final rowBooks = List<BookModel?>.generate(
                        _booksPerRow,
                        (index) {
                          final itemIndex = startIndex + index;
                          return itemIndex < books.length ? books[itemIndex] : null;
                        },
                      );

                      return LayoutBuilder(
                        builder: (context, constraints) {
                          final cardWidth = (constraints.maxWidth -
                                  (_columnGap * (_booksPerRow - 1))) /
                              _booksPerRow;
                          final cardHeight = cardWidth / _childAspectRatio;

                          return Column(
                            mainAxisSize: MainAxisSize.min,
                            crossAxisAlignment: CrossAxisAlignment.stretch,
                            children: [
                              SizedBox(
                                height: cardHeight,
                                child: Row(
                                  crossAxisAlignment: CrossAxisAlignment.end,
                                  children: [
                                    for (var index = 0; index < rowBooks.length; index++) ...[
                                      Expanded(
                                        child: rowBooks[index] != null
                                            ? Align(
                                                alignment: Alignment.bottomCenter,
                                                child: BookCard(
                                                  book: _toBook(rowBooks[index]!),
                                                  onTap: () => _openBook(
                                                    context,
                                                    rowBooks[index]!,
                                                  ),
                                                ),
                                              )
                                            : const SizedBox.shrink(),
                                      ),
                                      if (index < rowBooks.length - 1)
                                        const SizedBox(width: _columnGap),
                                    ],
                                  ],
                                ),
                              ),
                              Container(
                                height: 8,
                                width: double.infinity,
                                color: const Color(0xFFC18A53),
                              ),
                              Container(
                                height: 22,
                                width: double.infinity,
                                decoration: BoxDecoration(
                                  color: const Color(0xFF5C3D1E),
                                  boxShadow: const [
                                    BoxShadow(
                                      color: Colors.black,
                                      blurRadius: 10,
                                      offset: Offset(0, 5),
                                    ),
                                  ],
                                ),
                              ),
                              if (rowIndex < rowCount - 1)
                                const SizedBox(height: _gapBetweenShelves),
                            ],
                          );
                        },
                      );
                    },
                    childCount: rowCount,
                  ),
                ),
              ),
            ],
          );
        },
      ),
    );
  }

  void _openBook(BuildContext context, BookModel book) {
    Navigator.of(context).push(
      MaterialPageRoute<void>(
        builder: (_) => BookDetailPage(book: book),
      ),
    );
  }

  Book _toBook(BookModel model) {
    return Book(
      title: model.title,
      coverColor: _coverPalette[model.id % _coverPalette.length],
    );
  }
}
