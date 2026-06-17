class BookModel {
  const BookModel({
    required this.id,
    required this.title,
    required this.content,
    this.description,
    required this.category,
    required this.unlockCost,
    required this.isLocked,
  });

  final int id;
  final String title;
  final String content;
  final String? description;
  final String category;
  final int unlockCost;
  final bool isLocked;

  factory BookModel.fromJson(Map<String, dynamic> json) {
    return BookModel(
      id: json['id'] as int,
      title: json['title'] as String,
      content: json['content'] as String,
      description: json['description'],
      category: json['category'] as String,
      unlockCost: (json['unlockCost'] ?? json['unlock_cost']) as int,
      isLocked: (json['isLocked'] ?? json['is_locked']) as bool,
    );
  }
}
