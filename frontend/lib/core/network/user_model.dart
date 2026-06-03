class UserModel {
  const UserModel({
    required this.id,
    required this.totalPoints,
  });

  final String id;
  final int totalPoints;

  factory UserModel.fromJson(Map<String, dynamic> json) {
    return UserModel(
      id: json['id'] as String,
      totalPoints: (json['totalPoints'] ?? json['total_points']) as int,
    );
  }
}
