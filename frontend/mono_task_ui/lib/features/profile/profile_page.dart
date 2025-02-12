import 'package:flutter/widgets.dart';
import 'package:flutter/material.dart';
import 'package:mono_task_ui/core/utils/api/client_api.dart';
import 'package:mono_task_ui/features/profile/models/user_profile.dart';

// on login get summary
//
//
class ProfilePage extends StatefulWidget {
  const ProfilePage({Key? key, required this.userId}) : super(key: key);

  final int userId;

  @override
  State<ProfilePage> createState() => _ProfilePageState();
}

class _ProfilePageState extends State<ProfilePage> {
  bool isBusy = false;

  final apiClient = ClientApi(
    baseUrl: 'https://localhost:7248/api',
  );

  @override
  void initState() {
    super.initState();
    // Call getUserData when the page is loaded
    getUserData(widget.userId);
  }

  UserProfile? userProfile;

  Future<void> getUserData(userId) async {
    try {
      isBusy = true;
      //await apiClient.getRoute('', id: userId).then((response) {
      await apiClient.getQuery('', params: {'id': userId}).then((response) {
        setState(() {
          isBusy = false;
          userProfile = UserProfile.fromJson(response?.data);
        });
      });
    } catch (e) {
      print("Error: $e");
    }
  }

  Widget _userId() {
    return Text(widget.userId.toString());
  }

  Widget _userFullName() {
    return Text("${userProfile?.fullName}");
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(),
      body: Column(
        children: <Widget>[
          const Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              CircleAvatar(
                maxRadius: 65,
                backgroundImage:
                    AssetImage('assets/images/profile_default.png'),
              ),
            ],
          ),
        ],
      ),
    );
  }
}
