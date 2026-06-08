import 'dart:convert';

import 'package:http/http.dart' as http;

import 'book_model.dart';
import 'question_model.dart';
import 'user_model.dart';

class ApiService {
  static const String baseUrl = 'https://okuma-uygulamasi-api.onrender.com';

  Future<List<BookModel>> getBooks() async {
    try {
      final response = await http.get(Uri.parse('$baseUrl/api/v1/books'));
      if (response.statusCode != 200) {
        throw Exception('Failed to load books: ${response.statusCode}');
      }

      final dynamic decoded = jsonDecode(response.body);
      if (decoded is! List) {
        throw Exception('Invalid books response format');
      }

      return decoded
          .map((item) => BookModel.fromJson(item as Map<String, dynamic>))
          .toList();
    } catch (e) {
      throw Exception('getBooks error: $e');
    }
  }

  Future<List<QuestionModel>> getQuestionsByBook(int bookId) async {
    try {
      final response = await http.get(
        Uri.parse('$baseUrl/api/v1/questions/book/$bookId'),
      );
      if (response.statusCode != 200) {
        throw Exception('Failed to load questions: ${response.statusCode}');
      }

      final dynamic decoded = jsonDecode(response.body);
      if (decoded is! List) {
        throw Exception('Invalid questions response format');
      }

      return decoded
          .map((item) => QuestionModel.fromJson(item as Map<String, dynamic>))
          .toList();
    } catch (e) {
      throw Exception('getQuestionsByBook error: $e');
    }
  }

  Future<UserModel> getUser(String deviceId) async {
    try {
      final response = await http.get(
        Uri.parse('$baseUrl/api/v1/users/$deviceId'),
      );
      if (response.statusCode != 200) {
        throw Exception('Failed to load user: ${response.statusCode}');
      }

      final dynamic decoded = jsonDecode(response.body);
      if (decoded is! Map<String, dynamic>) {
        throw Exception('Invalid user response format');
      }

      return UserModel.fromJson(decoded);
    } catch (e) {
      throw Exception('getUser error: $e');
    }
  }

  Future<bool> answerQuestion(
    int questionId,
    String deviceId,
    String userAnswer,
  ) async {
    try {
      final response = await http.post(
        Uri.parse('$baseUrl/api/v1/questions/$questionId/answer'),
        headers: {
          'Content-Type': 'application/json',
          'Accept': 'application/json',
        },
        body: jsonEncode({"userId": deviceId, "userAnswer": userAnswer}),
      );

      if (response.statusCode == 200 || response.statusCode == 201) {
        return true;
      } else {
        throw Exception(response.body);
      }
    } catch (e) {
      throw Exception('Bağlantı Hatası: $e');
    }
  }

  Future<List<int>> getUnlockedBookIds(String userId) async {
    try {
      final response = await http.get(
        Uri.parse('$baseUrl/api/v1/users/$userId/unlocked-books'),
      );
      if (response.statusCode != 200) {
        throw Exception(
          'Failed to load unlocked books: ${response.statusCode}',
        );
      }

      final dynamic decoded = jsonDecode(response.body);
      if (decoded is! List) {
        throw Exception('Invalid unlocked-books response format');
      }

      return decoded.map((item) {
        if (item is int) return item;
        if (item is Map<String, dynamic>) {
          return (item['id'] ?? item['bookId'] ?? item['book_id']) as int;
        }
        return item as int;
      }).toList();
    } catch (e) {
      throw Exception('getUnlockedBookIds error: $e');
    }
  }

  Future<bool> unlockBook(String deviceId, int bookId) async {
    try {
      final url = Uri.parse(
        '$baseUrl/api/v1/users/unlock-book?userId=$deviceId&bookId=$bookId',
      );

      final response = await http.post(url, headers: {'Accept': '*/*'});

      if (response.statusCode == 200 || response.statusCode == 201) {
        return true;
      } else {
        throw Exception(response.body);
      }
    } catch (e) {
      throw Exception('Bağlantı Hatası: $e');
    }
  }
}
