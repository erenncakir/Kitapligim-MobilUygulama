import 'package:flutter/material.dart';

class BookShelf extends StatelessWidget {
  const BookShelf({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 15,
      decoration: const BoxDecoration(
        color: Color(0xFF5C3D1E),
        boxShadow: [
          BoxShadow(
            color: Colors.black54,
            blurRadius: 6,
            offset: Offset(0, 4),
          ),
        ],
      ),
    );
  }
}
