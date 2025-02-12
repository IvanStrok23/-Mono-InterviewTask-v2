import 'package:flutter/material.dart';
import 'package:mono_task_ui/shared/models/vehicle_model.dart';

class VehicleTable extends StatelessWidget {
  final List<VehicleModel> vehicles;

  const VehicleTable({Key? key, required this.vehicles}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
      scrollDirection: Axis.horizontal,
      child: DataTable(
        columns: const [
          DataColumn(label: Text('ID')),
          DataColumn(label: Text('Name')),
          DataColumn(label: Text('Brand')),
          DataColumn(label: Text('Year')),
          DataColumn(label: Text('Action')),
        ],
        rows: vehicles.map((vehicle) {
          return DataRow(cells: [
            DataCell(Text(vehicle.id.toString())),
            DataCell(Text(vehicle.name)),
            DataCell(Text(vehicle.brandName)),
            DataCell(Text(vehicle.year.toString())),
            DataCell(
              ElevatedButton(
                onPressed: true
                    ? null
                    : () {
                        // Handle button press
                        ScaffoldMessenger.of(context).showSnackBar(
                          SnackBar(content: Text('${vehicle.name} selected')),
                        );
                      },
                child: Text(true ? 'Disabled' : 'Select'),
              ),
            ),
          ]);
        }).toList(),
      ),
    );
  }
}
