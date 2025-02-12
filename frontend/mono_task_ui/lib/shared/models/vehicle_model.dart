class VehicleModel {
  final int id;
  final String name;
  final int? brandId;
  final String brandName;
  final int year;

  VehicleModel({
    required this.id,
    required this.name,
    this.brandId,
    required this.brandName,
    required this.year,
  });

  factory VehicleModel.fromJson(Map<String, dynamic> json) {
    return VehicleModel(
      id: json['id'],
      name: json['name'],
      brandId: json['brandId'],
      brandName: json['brandName'],
      year: json['year'], // Defaulting to false if not provided
    );
  }
}
