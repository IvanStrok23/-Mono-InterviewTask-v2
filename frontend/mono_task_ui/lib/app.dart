import 'package:flutter/material.dart';
import 'package:mono_task_ui/core/auth/custom_auth.dart';
import 'package:mono_task_ui/core/utils/routh/router.dart';
import 'package:mono_task_ui/core/utils/settings/settings_controller.dart';

class MyApp extends StatelessWidget {
  const MyApp({super.key, required this.settingsController});
  final SettingsController settingsController;

  @override
  Widget build(BuildContext context) {
    final CustomAuth auth = CustomAuth();

    return AnimatedBuilder(
      builder: (BuildContext context, Widget? child) {
        return MaterialApp.router(
          locale: const Locale('bs', 'BA'),
          restorationScopeId: 'app',
          debugShowCheckedModeBanner: false,
          theme: ThemeData(),
          darkTheme: ThemeData.dark(),
          routerConfig: createRouter(auth),
        );
      },
      animation: settingsController,
    );
  }
}
