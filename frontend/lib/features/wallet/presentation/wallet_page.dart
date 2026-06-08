import 'package:flutter/material.dart';

import '../../../core/network/api_service.dart';
import '../../../core/network/user_model.dart';

class WalletPage extends StatefulWidget {
  const WalletPage({super.key});

  @override
  State<WalletPage> createState() => _WalletPageState();
}

class _WalletPageState extends State<WalletPage> {
  int _balance = 0;
  static const String _deviceId = 'test-device-2';

  @override
  void initState() {
    super.initState();
    _loadBalance();
  }

  Future<void> _loadBalance() async {
    try {
      final UserModel user = await ApiService().getUser(_deviceId);
      if (mounted) {
        setState(() {
          _balance = user.totalPoints;
        });
      }
    } catch (_) {
      // ignore
    }
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: SingleChildScrollView(
        padding: const EdgeInsets.fromLTRB(16, 12, 16, 24),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Container(
              width: double.infinity,
              padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 14),
              decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(16),
                gradient: const LinearGradient(
                  colors: [Color(0xFF1F2937), Color(0xFF0F172A)],
                ),
              ),
              child: Text(
                '🪙 Mevcut Bakiyen: $_balance Jeton',
                style: TextStyle(
                  color: Colors.white,
                  fontSize: 18,
                  fontWeight: FontWeight.w700,
                ),
              ),
            ),
            const SizedBox(height: 20),
            GestureDetector(
              onTap: _loadBalance,
              child: const Icon(Icons.refresh, size: 20, color: Colors.grey),
            ),
            const SizedBox(height: 12),
            const Text(
              'Jeton Paketleri - Gerçek Para ile Satın Al',
              style: TextStyle(fontSize: 20, fontWeight: FontWeight.w800),
            ),
            const SizedBox(height: 14),
            GridView.count(
              crossAxisCount: 2,
              crossAxisSpacing: 12,
              mainAxisSpacing: 12,
              shrinkWrap: true,
              physics: const NeverScrollableScrollPhysics(),
              childAspectRatio: 0.78,
              children: const [
                _PackageCard(
                  title: '500 Jeton',
                  buttonLabel: '49.99 TL - Satın Al',
                  colors: [Color(0xFF60A5FA), Color(0xFF2563EB)],
                ),
                _PackageCard(
                  title: '1200 Jeton (Best Value)',
                  buttonLabel: '99.99 TL - Satın Al',
                  colors: [Color(0xFF4ADE80), Color(0xFF15803D)],
                ),
                _PackageCard(
                  title: '2500 Jeton',
                  buttonLabel: '199.99 TL - Satın Al',
                  colors: [Color(0xFFFB7185), Color(0xFFE11D48)],
                ),
                _PackageCard(
                  title: '5000 Jeton',
                  buttonLabel: '399.99 TL - Satın Al',
                  colors: [Color(0xFFC084FC), Color(0xFF7C3AED)],
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}

class _PackageCard extends StatelessWidget {
  const _PackageCard({
    required this.title,
    required this.buttonLabel,
    required this.colors,
  });

  final String title;
  final String buttonLabel;
  final List<Color> colors;

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(16),
        gradient: LinearGradient(
          begin: Alignment.topLeft,
          end: Alignment.bottomRight,
          colors: colors,
        ),
      ),
      child: Padding(
        padding: const EdgeInsets.fromLTRB(10, 12, 10, 10),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            const SizedBox(height: 2),
            const _StackedCoinIcon(),
            Text(
              title,
              textAlign: TextAlign.center,
              style: const TextStyle(
                color: Colors.white,
                fontSize: 16,
                fontWeight: FontWeight.w800,
              ),
            ),
            SizedBox(
              width: double.infinity,
              child: ElevatedButton(
                onPressed: () {},
                style: ElevatedButton.styleFrom(
                  backgroundColor: Colors.white,
                  foregroundColor: Colors.black87,
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(12),
                  ),
                  padding: const EdgeInsets.symmetric(vertical: 10),
                ),
                child: Text(
                  buttonLabel,
                  textAlign: TextAlign.center,
                  style: const TextStyle(
                    fontWeight: FontWeight.w700,
                    fontSize: 12.5,
                  ),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}

class _StackedCoinIcon extends StatelessWidget {
  const _StackedCoinIcon();

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: 72,
      height: 58,
      child: Stack(
        alignment: Alignment.center,
        children: const [
          Positioned(
            left: 10,
            top: 18,
            child: Icon(
              Icons.monetization_on_rounded,
              size: 30,
              color: Color(0xFFFFF176),
            ),
          ),
          Positioned(
            right: 10,
            top: 18,
            child: Icon(
              Icons.monetization_on_rounded,
              size: 30,
              color: Color(0xFFFFD54F),
            ),
          ),
          Positioned(
            top: 0,
            child: Icon(
              Icons.monetization_on_rounded,
              size: 34,
              color: Color(0xFFFFEE58),
            ),
          ),
        ],
      ),
    );
  }
}
