import 'package:flutter/material.dart';
import 'custom_auth.dart';

class AuthListener extends ChangeNotifier {
  final CustomAuth auth;

  AuthListener(this.auth) {
    auth.authStateChanges.listen((_) {
      notifyListeners();
    });
  }
}
