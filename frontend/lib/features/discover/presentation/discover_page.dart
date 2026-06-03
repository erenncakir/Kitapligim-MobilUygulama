import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

import '../../../core/network/api_service.dart';
import '../../../core/network/book_model.dart';
import '../../../core/theme/app_colors.dart';
import '../../library/presentation/widgets/book_card.dart';
import 'widgets/discover_book_tile.dart';

class DiscoverPage extends StatefulWidget {
  const DiscoverPage({super.key});

  @override
  State<DiscoverPage> createState() => _DiscoverPageState();
}

class _DiscoverPageState extends State<DiscoverPage> {
  static const double _horizontalPad = 16;
  static const double _fieldRadius = 16;
  static const double _gridGap = 12;

  final List<String> _categories = const [
    'Tümü',
    "İstanbulun Mirasları",
    'Denizin Derinlikleri',
    'Uzayın Gizemleri',
  ];

  String _selectedCategory = 'Tümü';

  List<BookModel> _filterBooks(List<BookModel> books) {
    final selectedCategory = _selectedCategory;
    return books.where((b) {
      if (selectedCategory == 'Tümü') return true;
      String keyword = selectedCategory.split(' ')[0].toLowerCase();
      return b.category.toLowerCase().contains(keyword);
    }).toList();
  }

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
          final allBooks = snapshot.data ?? const <BookModel>[];
          final filteredBooks = _filterBooks(allBooks);

          return CustomScrollView(
            physics: const BouncingScrollPhysics(
              parent: AlwaysScrollableScrollPhysics(),
            ),
            slivers: [
              SliverPadding(
                padding: const EdgeInsets.fromLTRB(
                  _horizontalPad,
                  16,
                  _horizontalPad,
                  12,
                ),
                sliver: SliverToBoxAdapter(
                  child: TextField(
                    style: GoogleFonts.nunito(color: Colors.white),
                    cursorColor: AppColors.gold,
                    decoration: InputDecoration(
                      hintText: 'Yeni maceralar ara...',
                      hintStyle: GoogleFonts.nunito(
                        color: Colors.white54,
                        fontWeight: FontWeight.w500,
                      ),
                      filled: true,
                      fillColor: AppColors.woodShelf.withValues(alpha: 0.55),
                      contentPadding: const EdgeInsets.symmetric(
                        horizontal: 18,
                        vertical: 16,
                      ),
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(_fieldRadius),
                        borderSide: BorderSide.none,
                      ),
                      enabledBorder: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(_fieldRadius),
                        borderSide: BorderSide(
                          color: AppColors.gold.withValues(alpha: 0.25),
                        ),
                      ),
                      focusedBorder: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(_fieldRadius),
                        borderSide: const BorderSide(
                          color: AppColors.gold,
                          width: 1.5,
                        ),
                      ),
                      prefixIcon: const Icon(
                        Icons.search_rounded,
                        color: AppColors.gold,
                      ),
                    ),
                  ),
                ),
              ),
              SliverPadding(
                padding: const EdgeInsets.fromLTRB(
                  _horizontalPad,
                  0,
                  _horizontalPad,
                  12,
                ),
                sliver: SliverToBoxAdapter(
                  child: SizedBox(
                    height: 40,
                    child: ListView.separated(
                      scrollDirection: Axis.horizontal,
                      itemCount: _categories.length,
                      separatorBuilder: (_, _) => const SizedBox(width: 8),
                      itemBuilder: (context, index) {
                        final category = _categories[index];
                        final isSelected = _selectedCategory == category;
                        return FilterChip(
                          selected: isSelected,
                          showCheckmark: false,
                          label: Text(category),
                          labelStyle: TextStyle(
                            color: isSelected ? Colors.black : Colors.white,
                            fontWeight: FontWeight.w600,
                          ),
                          backgroundColor: AppColors.woodShelf.withValues(
                            alpha: 0.4,
                          ),
                          selectedColor: AppColors.gold,
                          shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(20),
                          ),
                          onSelected: (_) =>
                              setState(() => _selectedCategory = category),
                        );
                      },
                    ),
                  ),
                ),
              ),
              SliverPadding(
                padding: const EdgeInsets.fromLTRB(
                  _horizontalPad,
                  8,
                  _horizontalPad,
                  24,
                ),
                sliver: SliverGrid(
                  gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
                    crossAxisCount: 2,
                    crossAxisSpacing: _gridGap,
                    mainAxisSpacing: _gridGap,
                    childAspectRatio: BookCard.aspectRatio,
                  ),
                  delegate: SliverChildBuilderDelegate((context, index) {
                    return DiscoverBookTile(book: filteredBooks[index]);
                  }, childCount: filteredBooks.length),
                ),
              ),
            ],
          );
        },
      ),
    );
  }
}
