class RegisterUserRequest {
  String name;
  String email;
  String password;

  RegisterUserRequest(
      {required this.name, required this.email, required this.password});

  Map<String, dynamic> toJson() {
    return {
      'name': name,
      'email': email,
      'password': password,
    };
  }
}
