import 'package:flutter/material.dart';

/// Satın alma sonrası kitaplığın API yanıtını beklemeden güncellenmesi için.
final ValueNotifier<Set<int>> optimisticallyUnlockedBookIds =
    ValueNotifier<Set<int>>({});

void markBookOptimisticallyUnlocked(int bookId) {
  optimisticallyUnlockedBookIds.value = {
    ...optimisticallyUnlockedBookIds.value,
    bookId,
  };
}
