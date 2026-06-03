import 'package:flutter/material.dart';

import 'core/theme/app_theme.dart';
import 'features/shell/presentation/main_shell_page.dart';

class OkumaApp extends StatelessWidget {
  const OkumaApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Okuma',
      debugShowCheckedModeBanner: false,
      theme: AppTheme.dark(),
      home: const MainShellPage(),
    );
  }
}
