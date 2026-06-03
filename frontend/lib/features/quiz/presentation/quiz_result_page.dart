import 'package:flutter/material.dart';

import '../../../core/network/api_service.dart';
import '../../../core/network/question_model.dart';
import '../../../core/state/token_notifier.dart';

class QuizResultPage extends StatefulWidget {
  const QuizResultPage({
    super.key,
    required this.questions,
    required this.selectedAnswers,
  });

  final List<QuestionModel> questions;
  final List<String?> selectedAnswers;

  @override
  State<QuizResultPage> createState() => _QuizResultPageState();
}

class _QuizResultPageState extends State<QuizResultPage> {
  int _correctCount = 0;
  int _earnedTokens = 0;

  @override
  void initState() {
    super.initState();
    _correctCount = _calculateCorrectCount();
    _earnedTokens = _calculateEarnedTokens();
    _submitCorrectAnswers();
  }

  int _calculateCorrectCount() {
    var count = 0;
    for (var i = 0; i < widget.questions.length; i++) {
      if (widget.selectedAnswers[i] == widget.questions[i].correctAnswer) {
        count += 1;
      }
    }
    return count;
  }

  int _calculateEarnedTokens() {
    var total = 0;
    for (var i = 0; i < widget.questions.length; i++) {
      if (widget.selectedAnswers[i] == widget.questions[i].correctAnswer) {
        total += widget.questions[i].points;
      }
    }
    return total;
  }

  final _apiService = ApiService();
  final String _userId = 'test-device-1';

  Future<void> _submitCorrectAnswers() async {
    for (int i = 0; i < widget.questions.length; i++) {
      final question = widget.questions[i];
      final answer = widget.selectedAnswers[i];

      if (answer == question.correctAnswer) {
        await _apiService.answerQuestion(question.id, _userId, answer!);
      }
    }

    try {
      final user = await _apiService.getUser(_userId);
      tokenBalanceNotifier.value = user.totalPoints;
    } catch (e) {
      debugPrint('Bakiye çekilemedi: $e');
    }
  }

  @override
  Widget build(BuildContext context) {
    final wrongCount = widget.questions.length - _correctCount;

    return Scaffold(
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.fromLTRB(16, 20, 16, 20),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              const SizedBox(height: 10),
              const Center(
                child: Text(
                  '🏆',
                  style: TextStyle(
                    fontSize: 88,
                    shadows: [Shadow(color: Color(0xAAFFD54F), blurRadius: 30)],
                  ),
                ),
              ),
              const SizedBox(height: 12),
              const Text(
                'Tebrikler!',
                textAlign: TextAlign.center,
                style: TextStyle(fontSize: 34, fontWeight: FontWeight.w900),
              ),
              const SizedBox(height: 28),
              Container(
                padding: const EdgeInsets.fromLTRB(18, 20, 18, 20),
                decoration: BoxDecoration(
                  color: Colors.white,
                  borderRadius: BorderRadius.circular(20),
                  border: Border.all(color: Colors.grey.shade300),
                  boxShadow: const [
                    BoxShadow(
                      color: Color(0x22000000),
                      blurRadius: 14,
                      offset: Offset(0, 6),
                    ),
                  ],
                ),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      '✅ Dogru: $_correctCount',
                      style: const TextStyle(
                        fontSize: 21,
                        fontWeight: FontWeight.w800,
                      ),
                    ),
                    const SizedBox(height: 10),
                    Text(
                      '❌ Yanlis: $wrongCount',
                      style: const TextStyle(
                        fontSize: 21,
                        fontWeight: FontWeight.w800,
                      ),
                    ),
                    const SizedBox(height: 10),
                    Text(
                      '💰 Kazanilan Jeton: $_earnedTokens',
                      style: const TextStyle(
                        fontSize: 21,
                        fontWeight: FontWeight.w800,
                      ),
                    ),
                  ],
                ),
              ),
              const Spacer(),
              SizedBox(
                height: 56,
                child: ElevatedButton(
                  onPressed: () {
                    Navigator.of(context).popUntil((route) => route.isFirst);
                  },
                  style: ElevatedButton.styleFrom(
                    backgroundColor: const Color(0xFF2563EB),
                    foregroundColor: Colors.white,
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(14),
                    ),
                  ),
                  child: const Text(
                    'Kutuphaneye Don',
                    style: TextStyle(fontSize: 18, fontWeight: FontWeight.w800),
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
