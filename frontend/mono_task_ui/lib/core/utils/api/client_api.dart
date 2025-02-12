import 'package:dio/dio.dart';
import 'package:mono_task_ui/core/utils/api/api.dart';

class ClientApi {
  final String baseUrl;
  ClientApi({required this.baseUrl});
  Api api = Api();

  Future<dynamic> get(String endpoint, {dynamic body}) async {
    final response = await api.api.get(
      '$baseUrl/$endpoint',
      data: body,
    );
    return response;
  }

  Future<dynamic> getRoute(String endpoint, {required id}) async {
    endpoint = endpoint == '' ? '' : '$endpoint/';
    final path = '$baseUrl/$endpoint$id';
    final response = await api.api.get(path);
    return response;
  }

  Future<dynamic> getQuery(String endpoint, {dynamic params}) async {
    final response = await api.api.get('$baseUrl/$endpoint',
        queryParameters: params,
        options: Options(headers: {'content-Type': 'application/json'}));
    return response;
  }

  Future<dynamic> post(String endpoint, {dynamic body}) async {
    final response = await api.api.post(
      '$baseUrl/$endpoint',
      data: body,
    );
    return response;
  }
}
