import 'package:flutter/material.dart';

import '../../../core/network/api_service.dart';
import '../../../core/network/book_model.dart';
import '../../../core/state/optimistic_unlock_notifier.dart';
import '../../discover/presentation/book_detail_page.dart';
import '../models/book.dart';
import 'widgets/book_card.dart';

class MyLibraryPage extends StatefulWidget {
  const MyLibraryPage({super.key});

  @override
  State<MyLibraryPage> createState() => _MyLibraryPageState();
}

class _MyLibraryPageState extends State<MyLibraryPage> {
  static const String _deviceId = 'test-device-2';
  static const double _horizontalPad = 16;
  static const double _columnGap = 20;
  static const double _gapBetweenShelves = 30;
  static const double _childAspectRatio = 0.65;
  static const int _booksPerRow = 2;

  List<BookModel>? _allBooks;
  bool _isLoading = true;
  String? _error;

  @override
  void initState() {
    super.initState();
    optimisticallyUnlockedBookIds.addListener(_onOptimisticUnlock);
    _loadBooks();
  }

  @override
  void dispose() {
    optimisticallyUnlockedBookIds.removeListener(_onOptimisticUnlock);
    super.dispose();
  }

  void _onOptimisticUnlock() {
    if (_allBooks == null) return;
    setState(() {
      _allBooks = BookModel.applyUnlockedStatus(
        _allBooks!,
        optimisticallyUnlockedBookIds.value,
      );
    });
  }

  Future<List<BookModel>> _fetchBooks() async {
    final api = ApiService();
    final results = await Future.wait([
      api.getBooks(),
      api.getUnlockedBookIds(_deviceId),
    ]);
    final books = results[0] as List<BookModel>;
    final unlockedIds = Set<int>.from(results[1] as List<int>);
    final mergedUnlocked = {...unlockedIds, ...optimisticallyUnlockedBookIds.value};
    return BookModel.applyUnlockedStatus(books, mergedUnlocked);
  }

  Future<void> _loadBooks({bool showLoading = true}) async {
    if (showLoading && _allBooks == null) {
      setState(() {
        _isLoading = true;
        _error = null;
      });
    }

    try {
      final books = await _fetchBooks();
      if (!mounted) return;
      setState(() {
        _allBooks = books;
        _isLoading = false;
        _error = null;
      });
    } catch (_) {
      if (!mounted) return;
      setState(() {
        _isLoading = false;
        _error = 'Kitaplar yüklenemedi';
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return ColoredBox(
      color: Theme.of(context).scaffoldBackgroundColor,
      child: _buildBody(context),
    );
  }

  Widget _buildBody(BuildContext context) {
    if (_isLoading && _allBooks == null) {
      return const Center(child: CircularProgressIndicator());
    }
    if (_error != null && _allBooks == null) {
      return Center(
        child: Text(
          _error!,
          style: Theme.of(context).textTheme.titleMedium,
        ),
      );
    }

    final books =
        (_allBooks ?? const <BookModel>[])
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
            delegate: SliverChildBuilderDelegate((context, rowIndex) {
              final startIndex = rowIndex * _booksPerRow;
              final rowBooks = List<BookModel?>.generate(_booksPerRow, (index) {
                final itemIndex = startIndex + index;
                return itemIndex < books.length ? books[itemIndex] : null;
              });

              return LayoutBuilder(
                builder: (context, constraints) {
                  final cardWidth =
                      (constraints.maxWidth - (_columnGap * (_booksPerRow - 1))) /
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
            }, childCount: rowCount),
          ),
        ),
      ],
    );
  }

  Future<void> _openBook(BuildContext context, BookModel book) async {
    final unlocked = await Navigator.of(context).push<bool>(
      MaterialPageRoute<bool>(
        builder: (_) => BookDetailPage(book: book),
      ),
    );
    if (!mounted) return;
    if (unlocked == true) {
      markBookOptimisticallyUnlocked(book.id);
    }
    _loadBooks(showLoading: false);
  }

  Book _toBook(BookModel model) {
    return Book(title: model.title, imageUrl: model.imageUrl);
  }
}
