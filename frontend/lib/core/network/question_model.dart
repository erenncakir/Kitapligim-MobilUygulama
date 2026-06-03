class QuestionModel {
  const QuestionModel({
    required this.id,
    required this.bookId,
    required this.text,
    required this.options,
    required this.correctAnswer,
    required this.points,
  });

  final int id;
  final int bookId;
  final String text;
  final List<String> options;
  final String correctAnswer;
  final int points;

  factory QuestionModel.fromJson(Map<String, dynamic> json) {
    return QuestionModel(
      id: json['id'] as int,
      bookId: (json['bookId'] ?? json['book_id']) as int,
      text: json['text'] as String,
      options: (json['options'] as List<dynamic>).map((e) => '$e').toList(),
      correctAnswer:
          (json['correctAnswer'] ?? json['correct_answer']) as String,
      points: json['points'] as int,
    );
  }
}
