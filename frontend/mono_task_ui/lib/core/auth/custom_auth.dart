import 'dart:convert';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http;
import 'dart:async';

class CustomAuth {
  final FlutterSecureStorage storage = const FlutterSecureStorage();

  StreamController<String?> _authStateController = StreamController.broadcast();

  Stream<String?> get authStateChanges => _authStateController.stream;

  String? get currentUser => _currentUser;
  String? _currentUser;

  CustomAuth() {
    _initializeUser();
  }

  Future<void> _initializeUser() async {
    final accessToken = await _getValidAccessToken();
    if (accessToken != null) {
      _currentUser = accessToken;
    } else {
      _currentUser = null;
    }
    _authStateController.add(_currentUser);
  }

  Future<void> loginUser(
      {required String email, required String password}) async {
    final response = await http.post(
      Uri.parse('https://localhost:7248/api/auth/login'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode({'email': email, 'password': password}),
    );

    if (response.statusCode == 200) {
      final data = jsonDecode(response.body);

      final accessToken = data['accessToken'];
      final refreshToken = data['refreshToken'];
      final expiryTimeString = data['accessTokenExpiry'];

      await storage.write(key: 'access_token', value: accessToken);
      await storage.write(key: 'refresh_token', value: refreshToken);
      await storage.write(key: 'expiry_time', value: expiryTimeString);

      _currentUser = accessToken;
      _authStateController.add(_currentUser);
      return;
    } else {
      throw Exception('Failed to login: ${response.body}');
    }
  }

  Future<void> registerUser(
      {required String name,
      required String email,
      required String password}) async {
    final response = await http.post(
      Uri.parse('https://localhost:7248/api/auth/register'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode({'name': name, 'email': email, 'password': password}),
    );

    if (response.statusCode == 200) {
      final data = jsonDecode(response.body);

      final accessToken = data['accessToken'];
      final refreshToken = data['refreshToken'];
      final expiryTimeString = data['accessTokenExpiry'];

      await storage.write(key: 'access_token', value: accessToken);
      await storage.write(key: 'refresh_token', value: refreshToken);
      await storage.write(key: 'expiry_time', value: expiryTimeString);

      _currentUser = accessToken;
      _authStateController.add(_currentUser);

      return;
    } else {
      throw Exception('Failed to login: ${response.body}');
    }
  }

  Future<String?> refreshAccessToken() async {
    try {
      final accessToken = await _getValidAccessToken();
      if (accessToken != null) {
        _currentUser = accessToken;
        _authStateController.add(_currentUser);
        return accessToken;
      }

      return await _refreshToken();
    } catch (e) {
      print('Error refreshing token: $e');
      _authStateController.add(null);
      return null;
    }
  }

  Future<void> signOut() async {
    await storage.deleteAll();
    _currentUser = null;
    _authStateController.add(null);
  }

  Future<String?> _getValidAccessToken() async {
    final accessToken = await storage.read(key: 'access_token');
    final expiryTimeString = await storage.read(key: 'expiry_time');

    if (accessToken != null && expiryTimeString != null) {
      final expiryTime = DateTime.parse(expiryTimeString);
      if (DateTime.now().isBefore(expiryTime)) {
        return accessToken;
      }
    }
    return null;
  }

  Future<String?> _refreshToken() async {
    final refreshToken = await storage.read(key: 'refresh_token');
    if (refreshToken == null) {
      throw Exception('No refresh token found.');
    }

    final response = await http.post(
      Uri.parse('https://localhost:7248/api/auth/refresh-token'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode({'refreshToken': refreshToken}),
    );

    if (response.statusCode == 200) {
      final data = jsonDecode(response.body);
      final newAccessToken = data['accessToken'];
      final newRefreshToken = data['refreshToken'];
      final expiryTimeString = data['accessTokenExpiry'];

      await storage.write(key: 'access_token', value: newAccessToken);
      await storage.write(key: 'refresh_token', value: newRefreshToken);
      await storage.write(key: 'expiry_time', value: expiryTimeString);

      return newAccessToken;
    } else {
      throw Exception('Failed to refresh access token: ${response.body}');
    }
  }
}
