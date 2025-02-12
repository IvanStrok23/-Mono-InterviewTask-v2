import 'package:flutter/material.dart';
import 'package:mono_task_ui/core/auth/custom_auth.dart';
import 'package:mono_task_ui/core/auth/user_principal.dart';
import 'package:mono_task_ui/core/utils/api/client_api.dart';
import 'package:mono_task_ui/features/login/models/register_user_request.dart';
import 'package:mono_task_ui/features/login/widgets/helpers/login_widgets_enums.dart';
import 'package:mono_task_ui/features/login/widgets/input_field_widget.dart';
import 'package:mono_task_ui/shared/models/user_summary.dart';

class LoginPage extends StatefulWidget {
  const LoginPage({Key? key}) : super(key: key);

  @override
  State<LoginPage> createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  String? errorMessage = '';
  bool isLogin = true;
  // Principal principal = Principal();

  final apiClient = ClientApi(
    baseUrl: 'https://localhost:7248/api/user',
  );

  final TextEditingController _controllerEmail = TextEditingController();
  final TextEditingController _controllerPassword = TextEditingController();
  final TextEditingController _controllerName = TextEditingController();

  Future<void> loginUser() async {
    try {
      await CustomAuth().loginUser(
          email: _controllerEmail.text, password: _controllerPassword.text);
      await getSummary();
    } on Exception catch (e) {
      setState(() {
        errorMessage = e.toString();
      });
    }
  }

  Future<void> registerUser() async {
    try {
      await CustomAuth().registerUser(
          name: _controllerName.text,
          email: _controllerEmail.text,
          password: _controllerPassword.text);
      await getSummary();
    } on Exception catch (e) {
      setState(() {
        errorMessage = e.toString();
      });
    }
  }

  Future<void> getSummary() async {
    try {
      UserSummary? summary;
      await apiClient
          .get('summary', body: '')
          .then((response) => summary = UserSummary.fromJson(response?.data));
      UserPrincipal().set(summary);
    } on Exception catch (e) {
      print(e);
    }
  }

  Widget _title() {
    return const Text('Login');
  }

  Widget _errorMessage() {
    return Text(errorMessage ?? '');
  }

  Widget _submitButton() {
    return ElevatedButton(
        onPressed: isLogin ? loginUser : registerUser,
        child: Text(isLogin ? 'Login' : 'Register'));
  }

  Widget _loginOrRegisterButton() {
    return TextButton(
        onPressed: () {
          setState(() {
            isLogin = !isLogin;
          });
        },
        child: Text(isLogin ? 'Register instead' : 'Login instead'));
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: _title(),
        ),
        body: SingleChildScrollView(
          child: Container(
            height: MediaQuery.of(context).size.height,
            width: double.infinity,
            padding: const EdgeInsets.all(20),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.center,
              mainAxisAlignment: MainAxisAlignment.center,
              children: <Widget>[
                if (!isLogin)
                  InputFieldWidget(
                      textType: InputFieldWidgetType.name,
                      controller: _controllerName),
                InputFieldWidget(
                    textType: InputFieldWidgetType.email,
                    controller: _controllerEmail),
                InputFieldWidget(
                    textType: InputFieldWidgetType.password,
                    controller: _controllerPassword),
                _errorMessage(),
                _submitButton(),
                _loginOrRegisterButton()
              ],
            ),
          ),
        ));
  }
}
