import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:mono_task_ui/core/auth/user_principal.dart';
import 'package:mono_task_ui/core/utils/api/client_api.dart';
import 'package:mono_task_ui/core/utils/routh/router_utils.dart';
import 'package:mono_task_ui/shared/models/table_filter_data.dart';
import 'package:mono_task_ui/shared/models/vehicle_model.dart';
import 'package:mono_task_ui/shared/widgets/vehicle_table.dart';

class HomePage extends StatefulWidget {
  const HomePage({Key? key}) : super(key: key);

  @override
  _HomePageState createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  final apiClient = ClientApi(baseUrl: 'https://localhost:7248/api');
  final TextEditingController _controller =
      TextEditingController(); // Persistent search controller
  List<VehicleModel> vehicles = [];
  bool isLoading = true;

  @override
  void initState() {
    super.initState();
    fetchVehicleData(); // Initial fetch without search
  }

  @override
  void dispose() {
    _controller.dispose(); // Dispose the controller to prevent memory leaks
    super.dispose();
  }

  Future<void> fetchVehicleData({String searchValue = ''}) async {
    try {
      final filterData = TableFilterData(searchValue: searchValue);

      final response = await apiClient.getQuery(
        'vehicle-models', // API endpoint
        params: filterData.toQueryParams(),
      );

      final List<dynamic> data = response.data;

      setState(() {
        vehicles = data.map((json) => VehicleModel.fromJson(json)).toList();
        isLoading = false;
      });
    } catch (e) {
      setState(() {
        isLoading = false;
      });
      print('Error fetching vehicles: $e');
    }
  }

  Widget _title() {
    return const Text('Vehicle Models');
  }

  Widget _profileButton(BuildContext context) {
    return IconButton(
      icon: const Icon(Icons.account_circle,
          color: Color.fromARGB(255, 94, 94, 94), size: 34.0),
      onPressed: () {
        context.goNamed(
          APP_PAGE.profile.routeName,
          queryParameters: {'userId': UserPrincipal().id.toString()},
        );
      },
    );
  }

  Widget _searchBar() {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 10.0),
      child: TextField(
        controller: _controller,
        decoration: InputDecoration(
          hintText: 'Search vehicles...',
          border: OutlineInputBorder(borderRadius: BorderRadius.circular(12)),
          suffixIcon: IconButton(
            icon: const Icon(Icons.search),
            onPressed: () {
              setState(() {
                isLoading = true;
              });
              fetchVehicleData(searchValue: _controller.text);
            },
          ),
        ),
        onSubmitted: (value) {
          setState(() {
            isLoading = true;
          });
          fetchVehicleData(searchValue: value);
        },
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: _title(),
        actions: [_profileButton(context)],
      ),
      body: Padding(
        padding: const EdgeInsets.all(20.0),
        child: Column(
          children: [
            _searchBar(),
            Expanded(
              child: isLoading
                  ? const Center(child: CircularProgressIndicator())
                  : vehicles.isEmpty
                      ? const Center(child: Text('No vehicles found.'))
                      : VehicleTable(vehicles: vehicles),
            ),
          ],
        ),
      ),
    );
  }
}
