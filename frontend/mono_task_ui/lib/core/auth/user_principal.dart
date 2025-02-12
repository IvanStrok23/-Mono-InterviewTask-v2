import 'package:mono_task_ui/shared/models/user_summary.dart';

class UserPrincipal {
  static final UserPrincipal _instance = UserPrincipal._internal();

  factory UserPrincipal() {
    return _instance;
  }

  UserPrincipal._internal() {}

  UserSummary? _user;

  int get id {
    return _user?.id ?? -1;
  }

  String get name {
    return _user?.name ?? '';
  }

  String get email {
    return _user?.email ?? '';
  }

  void set(UserSummary? user) {
    _user = user;
  }
}
