class UserProfile {
  int id;
  String fullName;
  String email;
  int age;

  UserProfile(
      {required this.id,
      required this.email,
      required this.fullName,
      required this.age});

  factory UserProfile.fromJson(Map<String, dynamic> json) {
    return UserProfile(
      id: json['id'] ?? -1,
      email: json['email'] ?? '',
      fullName: json['fullName'] ?? '',
      age: json['age'] ?? -1,
    );
  }
}

class UserProfileRequest {
  String firstName;
  String lastName;
  String email;

  UserProfileRequest(
      {required this.firstName, required this.lastName, required this.email});

  Map<String, dynamic> toJson() {
    return {
      'firstName': firstName,
      'lastName': lastName,
      'email': email,
    };
  }
}
