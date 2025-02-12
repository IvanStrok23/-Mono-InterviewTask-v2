class UserSummary {
  int id;
  String name;
  String email;

  UserSummary({required this.id, required this.name, required this.email});

  factory UserSummary.fromJson(Map<String, dynamic> json) {
    return UserSummary(
      id: json['id'] ?? '',
      name: json['name'] ?? '',
      email: json['email'] ?? '',
    );
  }
}
