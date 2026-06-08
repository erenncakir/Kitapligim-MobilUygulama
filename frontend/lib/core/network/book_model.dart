class BookModel {
  const BookModel({
    required this.id,
    required this.title,
    required this.content,
    this.description,
    required this.category,
    this.imageUrl,
    required this.unlockCost,
    required this.isLocked,
  });

  final int id;
  final String title;
  final String content;
  final String? description;
  final String category;
  final String? imageUrl;
  final int unlockCost;
  final bool isLocked;

  factory BookModel.fromJson(Map<String, dynamic> json) {
    return BookModel(
      id: json['id'] as int,
      title: json['title'] as String,
      content: json['content'] as String,
      description: json['description'] as String?,
      category: json['category'] as String,
      imageUrl: (json['imageUrl'] ??
              json['image_url'] ??
              json['coverUrl'] ??
              json['cover_url'] ??
              json['image'])
          as String?,
      unlockCost: (json['unlockCost'] ?? json['unlock_cost']) as int,
      isLocked: (json['isLocked'] ?? json['is_locked']) as bool,
    );
  }

  BookModel withUnlockedOverride(Set<int> unlockedBookIds) {
    if (!unlockedBookIds.contains(id)) return this;
    return BookModel(
      id: id,
      title: title,
      content: content,
      description: description,
      category: category,
      imageUrl: imageUrl,
      unlockCost: unlockCost,
      isLocked: false,
    );
  }

  static List<BookModel> applyUnlockedStatus(
    List<BookModel> books,
    Set<int> unlockedBookIds,
  ) {
    return books
        .map((book) => book.withUnlockedOverride(unlockedBookIds))
        .toList();
  }
}
