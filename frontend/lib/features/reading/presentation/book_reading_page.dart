import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

import '../../../core/network/api_service.dart';
import '../../../core/network/book_model.dart';
import '../../../core/network/question_model.dart';
import '../../quiz/presentation/quiz_page.dart';

class BookReadingPage extends StatefulWidget {
  const BookReadingPage({
    super.key,
    required this.book,
  });

  final BookModel book;

  @override
  State<BookReadingPage> createState() => _BookReadingPageState();
}

class _BookReadingPageState extends State<BookReadingPage> {
  static const int _pageCharLimit = 800;
  static const double _minFontSize = 14;
  static const double _maxFontSize = 28;
  static const List<String> _fontFamilyLabels = ['Roboto', 'Lora', 'Nunito'];

  late final PageController _pageController;
  late List<String> _pages;
  int _currentPageIndex = 0;
  int _themeIndex = 0;
  int _fontFamilyIndex = 2;
  double _fontSize = 18;

  static const List<_ReadingTheme> _themes = [
    _ReadingTheme(
      scaffoldBg: Color(0xFFFDFBF7),
      pageBg: Color(0xFFF7F2E6),
      textColor: Color(0xFF111827),
      appBarFg: Color(0xFF111827),
    ),
    _ReadingTheme(
      scaffoldBg: Color(0xFFF4ECD8),
      pageBg: Color(0xFFECDDCC),
      textColor: Color(0xFF4B2E1E),
      appBarFg: Color(0xFF4B2E1E),
    ),
    _ReadingTheme(
      scaffoldBg: Color(0xFF1A1A2E),
      pageBg: Color(0xFF22223B),
      textColor: Color(0xFFE5E7EB),
      appBarFg: Color(0xFFE5E7EB),
    ),
  ];

  @override
  void initState() {
    super.initState();
    _pageController = PageController();
    _pages = _splitIntoPages(widget.book.content);
  }

  @override
  void didUpdateWidget(covariant BookReadingPage oldWidget) {
    super.didUpdateWidget(oldWidget);
    if (oldWidget.book.id != widget.book.id ||
        oldWidget.book.content != widget.book.content) {
      _pages = _splitIntoPages(widget.book.content);
      _currentPageIndex = 0;
      if (_pageController.hasClients) {
        _pageController.jumpToPage(0);
      }
    }
  }

  @override
  void dispose() {
    _pageController.dispose();
    super.dispose();
  }

  List<String> _splitIntoPages(String text) {
    final normalized = text.trim().replaceAll(RegExp(r'\s+'), ' ');
    if (normalized.isEmpty) {
      return const [''];
    }

    final words = normalized.split(' ');
    final pages = <String>[];
    final buffer = StringBuffer();

    for (final word in words) {
      final pending = buffer.isEmpty ? word : '${buffer.toString()} $word';
      if (pending.length > _pageCharLimit && buffer.isNotEmpty) {
        pages.add(buffer.toString());
        buffer
          ..clear()
          ..write(word);
      } else {
        buffer
          ..clear()
          ..write(pending);
      }
    }

    if (buffer.isNotEmpty) {
      pages.add(buffer.toString());
    }

    return pages.isEmpty ? const [''] : pages;
  }

  TextStyle _baseTextStyle(_ReadingTheme theme) {
    final base = TextStyle(
      color: theme.textColor,
      fontSize: _fontSize,
      height: 1.6,
    );

    switch (_fontFamilyIndex) {
      case 0:
        return GoogleFonts.roboto(textStyle: base);
      case 1:
        return GoogleFonts.lora(textStyle: base);
      case 2:
        return GoogleFonts.nunito(textStyle: base);
      default:
        return base;
    }
  }

  List<TextSpan> _parseBoldMarkdown(
    String text,
    TextStyle baseStyle,
    TextStyle boldStyle,
  ) {
    final spans = <TextSpan>[];
    final regex = RegExp(r'\*\*(.+?)\*\*');
    var lastEnd = 0;

    for (final match in regex.allMatches(text)) {
      if (match.start > lastEnd) {
        spans.add(
          TextSpan(
            text: text.substring(lastEnd, match.start),
            style: baseStyle,
          ),
        );
      }
      spans.add(TextSpan(text: match.group(1), style: boldStyle));
      lastEnd = match.end;
    }

    if (lastEnd < text.length) {
      spans.add(TextSpan(text: text.substring(lastEnd), style: baseStyle));
    }

    return spans.isEmpty ? [TextSpan(text: text, style: baseStyle)] : spans;
  }

  Widget _buildReadingText(String text, _ReadingTheme theme) {
    final baseStyle = _baseTextStyle(theme);
    final boldStyle = baseStyle.copyWith(fontWeight: FontWeight.w800);

    return RichText(
      text: TextSpan(
        style: baseStyle,
        children: _parseBoldMarkdown(text, baseStyle, boldStyle),
      ),
    );
  }

  void _changeFontSize(double delta) {
    setState(() {
      _fontSize = (_fontSize + delta).clamp(_minFontSize, _maxFontSize);
    });
  }

  void _cycleFontFamily() {
    setState(() {
      _fontFamilyIndex = (_fontFamilyIndex + 1) % _fontFamilyLabels.length;
    });
  }

  @override
  Widget build(BuildContext context) {
    final isLastPage = _currentPageIndex == _pages.length - 1;
    final activeTheme = _themes[_themeIndex];

    return Scaffold(
      backgroundColor: activeTheme.scaffoldBg,
      appBar: AppBar(
        backgroundColor: activeTheme.scaffoldBg,
        foregroundColor: activeTheme.appBarFg,
        surfaceTintColor: activeTheme.scaffoldBg,
        elevation: 0,
        title: Text(
          widget.book.title,
          textAlign: TextAlign.center,
          maxLines: 1,
          overflow: TextOverflow.ellipsis,
          style: TextStyle(
            color: activeTheme.appBarFg,
            fontWeight: FontWeight.w700,
          ),
        ),
        centerTitle: true,
        actions: [
          IconButton(
            tooltip: 'Yazıyı küçült',
            onPressed: _fontSize <= _minFontSize
                ? null
                : () => _changeFontSize(-2),
            icon: Icon(Icons.remove_rounded, color: activeTheme.appBarFg),
          ),
          IconButton(
            tooltip: 'Yazıyı büyüt',
            onPressed: _fontSize >= _maxFontSize
                ? null
                : () => _changeFontSize(2),
            icon: Icon(Icons.add_rounded, color: activeTheme.appBarFg),
          ),
          IconButton(
            tooltip: 'Font: ${_fontFamilyLabels[_fontFamilyIndex]}',
            onPressed: _cycleFontFamily,
            icon: Icon(Icons.font_download_rounded, color: activeTheme.appBarFg),
          ),
          IconButton(
            tooltip: 'Tema değiştir',
            onPressed: _toggleTheme,
            icon: Icon(Icons.palette_rounded, color: activeTheme.appBarFg),
          ),
        ],
      ),
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.fromLTRB(16, 12, 16, 20),
          child: Column(
            children: [
              Expanded(
                child: ClipRRect(
                  borderRadius: BorderRadius.circular(16),
                  child: ColoredBox(
                    color: activeTheme.pageBg,
                    child: PageView.builder(
                      controller: _pageController,
                      itemCount: _pages.length,
                      onPageChanged: (index) {
                        setState(() => _currentPageIndex = index);
                      },
                      itemBuilder: (context, index) {
                        return Padding(
                          padding: const EdgeInsets.fromLTRB(18, 22, 18, 22),
                          child: SingleChildScrollView(
                            child: _buildReadingText(_pages[index], activeTheme),
                          ),
                        );
                      },
                    ),
                  ),
                ),
              ),
              const SizedBox(height: 16),
              if (isLastPage)
                Padding(
                  padding: const EdgeInsets.only(bottom: 12),
                  child: SizedBox(
                    width: double.infinity,
                    height: 52,
                    child: ElevatedButton(
                      onPressed: _openQuiz,
                      style: ElevatedButton.styleFrom(
                        backgroundColor: const Color(0xFF22C55E),
                        foregroundColor: Colors.white,
                        shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(14),
                        ),
                      ),
                      child: const Text(
                        'Soruları Çöz & Jeton Kazan',
                        style: TextStyle(
                          fontSize: 17,
                          fontWeight: FontWeight.w800,
                        ),
                      ),
                    ),
                  ),
                ),
              Text(
                'Sayfa: ${_currentPageIndex + 1}/${_pages.length}',
                textAlign: TextAlign.center,
                style: TextStyle(
                  color: activeTheme.textColor.withValues(alpha: 0.85),
                  fontSize: 15,
                  fontWeight: FontWeight.w700,
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  void _toggleTheme() {
    setState(() {
      _themeIndex = (_themeIndex + 1) % _themes.length;
    });
  }

  Future<void> _openQuiz() async {
    showDialog<void>(
      context: context,
      barrierDismissible: false,
      builder: (_) => const Center(
        child: CircularProgressIndicator(),
      ),
    );

    try {
      final List<QuestionModel> questions =
          await ApiService().getQuestionsByBook(widget.book.id);
      if (!mounted) {
        return;
      }
      Navigator.of(context).pop();
      if (questions.isEmpty) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(content: Text('Bu kitap icin soru bulunamadi.')),
        );
        return;
      }
      Navigator.of(context).push(
        MaterialPageRoute<void>(
          builder: (_) => QuizPage(questions: questions),
        ),
      );
    } catch (_) {
      if (!mounted) {
        return;
      }
      Navigator.of(context).pop();
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Sorular yuklenemedi.')),
      );
    }
  }
}

class _ReadingTheme {
  const _ReadingTheme({
    required this.scaffoldBg,
    required this.pageBg,
    required this.textColor,
    required this.appBarFg,
  });

  final Color scaffoldBg;
  final Color pageBg;
  final Color textColor;
  final Color appBarFg;
}
