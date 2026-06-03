import 'package:flutter/material.dart';

/// Keşfet gridinde gösterilen kitap (kilitli veya açık).
class DiscoverListing {
  const DiscoverListing({
    required this.title,
    required this.coverColor,
    required this.isLocked,
    this.tokenCost,
  }) : assert(
          !isLocked || tokenCost != null,
          'Kilitli kitapta jeton bedeli gerekir.',
        );

  final String title;
  final Color coverColor;
  final bool isLocked;
  final int? tokenCost;
}
