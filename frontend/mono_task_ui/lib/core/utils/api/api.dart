import 'package:dio/dio.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:mono_task_ui/core/auth/custom_auth.dart';

class Api {
  final Dio api = Dio();
  String? accessToken;
  CustomAuth auth = CustomAuth();
  final _storage = const FlutterSecureStorage();

  Api() {
    api.interceptors
        .add(InterceptorsWrapper(onRequest: (options, handler) async {
      if (!options.path.contains('http')) {
        options.path = 'http://192.168.0.20:8080' + options.path;
      }
      accessToken ??= await _storage.read(key: "access_token");
      print(accessToken);
      options.headers['Authorization'] = 'Bearer $accessToken';

      return handler.next(options);
    }, onError: (DioException error, handler) async {
      if ((error.response?.statusCode == 401 &&
          error.response?.statusMessage == "Unauthorized")) {
        if (await onAuthError(error, handler)) {
          return handler.resolve(await _retry(error.requestOptions));
        }
      }
      return handler.next(error);
    }));
  }

  Future<bool> onAuthError(
      DioException error, ErrorInterceptorHandler handler) async {
    accessToken = await auth.refreshAccessToken();
    if (accessToken != null) {
      return true;
    } else {
      await unAuthorizate();
      return false;
    }
  }

  Future<Response<dynamic>> _retry(RequestOptions requestOptions) async {
    final options = Options(
      method: requestOptions.method,
      headers: requestOptions.headers,
    );
    return api.request<dynamic>(requestOptions.path,
        data: requestOptions.data,
        queryParameters: requestOptions.queryParameters,
        options: options);
  }

  Future<void> unAuthorizate() async {
    accessToken = null;
    await auth.signOut();
  }
}
