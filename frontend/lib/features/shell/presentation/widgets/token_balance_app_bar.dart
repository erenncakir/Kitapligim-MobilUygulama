import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

import '../../../../core/state/token_notifier.dart';
import '../../../../core/theme/app_colors.dart';

class TokenBalanceAppBar extends StatelessWidget implements PreferredSizeWidget {
  const TokenBalanceAppBar({super.key});

  static const double _radius = 14;

  @override
  Size get preferredSize => const Size.fromHeight(kToolbarHeight + 8);

  @override
  Widget build(BuildContext context) {
    return ValueListenableBuilder<int>(
      valueListenable: tokenBalanceNotifier,
      builder: (context, balance, child) {
        return AppBar(
          backgroundColor: AppColors.woodShelf,
          elevation: 0,
          centerTitle: true,
          title: const Text(''),
          actions: [
            Padding(
              padding: const EdgeInsets.only(right: 14),
              child: Container(
                padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 8),
                decoration: BoxDecoration(
                  color: AppColors.gold.withValues(alpha: 0.16),
                  borderRadius: BorderRadius.circular(_radius),
                  border: Border.all(
                    color: AppColors.gold.withValues(alpha: 0.45),
                  ),
                ),
                child: Row(
                  mainAxisSize: MainAxisSize.min,
                  children: [
                    const Icon(
                      Icons.monetization_on_rounded,
                      color: AppColors.gold,
                      size: 18,
                    ),
                    const SizedBox(width: 6),
                    Text(
                      '$balance 🪙',
                      style: GoogleFonts.nunito(
                        color: AppColors.gold,
                        fontSize: 14,
                        fontWeight: FontWeight.w800,
                        letterSpacing: 0.2,
                      ),
                    ),
                  ],
                ),
              ),
            ),
          ],
        );
      },
    );
  }
}
