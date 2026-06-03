import 'package:flutter/material.dart';

import '../models/discover_listing.dart';

/// API yerine önizleme: ilk 2 açık, son 2 kilitli (150 jeton).
final List<DiscoverListing> mockDiscoverListings = [
  const DiscoverListing(
    title: 'Denizaltı Gizemi',
    coverColor: Color(0xFF1565A8),
    isLocked: false,
  ),
  const DiscoverListing(
    title: 'Orman Arkadaşları',
    coverColor: Color(0xFF6D4C41),
    isLocked: false,
  ),
  const DiscoverListing(
    title: 'Sihirli Taş',
    coverColor: Color(0xFF7E3F9D),
    isLocked: true,
    tokenCost: 150,
  ),
  const DiscoverListing(
    title: 'Ejder İni',
    coverColor: Color(0xFFB85C00),
    isLocked: true,
    tokenCost: 150,
  ),
];
