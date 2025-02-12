import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

import 'package:mono_task_ui/app.dart';
import 'package:mono_task_ui/core/utils/settings/settings_controller.dart';
import 'package:mono_task_ui/core/utils/settings/settings_service.dart';
import 'package:provider/provider.dart';

void main() async {
  // Set up the SettingsController, which will glue user settings to multiple
  // Flutter Widgets.
  final settingsController = SettingsController(SettingsService());
  WidgetsFlutterBinding.ensureInitialized();

  // Load the user's preferred theme while the splash screen is displayed.
  // This prevents a sudden theme change when the app is first displayed.
  await settingsController.loadSettings();

  // Run the app and pass in the SettingsController. The app listens to the
  // SettingsController for changes, then passes it further down to the
  // SettingsView.

  SystemChrome.setPreferredOrientations([
    DeviceOrientation.portraitUp,
    DeviceOrientation.portraitDown,
  ]).then((_) {
    runApp(MyApp(settingsController: settingsController));
  });
}
