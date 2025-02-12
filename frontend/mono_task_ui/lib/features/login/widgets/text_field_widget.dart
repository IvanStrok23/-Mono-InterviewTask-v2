import 'package:flutter/material.dart';
import 'package:mono_task_ui/features/login/widgets/helpers/login_widgets_enums.dart';

class TextFieldWidget extends StatelessWidget {
  const TextFieldWidget({Key? key, required this.textType, this.textValue})
      : super(key: key);
  final String? textValue;
  final TextFieldWidgetType textType;

  String get labelString {
    String label;
    switch (textType) {
      case TextFieldWidgetType.name:
        label = "Name";
        break;
      case TextFieldWidgetType.email:
        label = "Email";
        break;
    }
    return textValue ?? label;
  }

  Icon get icon {
    switch (textType) {
      case TextFieldWidgetType.name:
        return const Icon(Icons.account_circle);
      case TextFieldWidgetType.email:
        return const Icon(Icons.email);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Center(
        child: Padding(
      padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 16),
      child: TextField(
        textAlign: TextAlign.center,
        style: const TextStyle(fontSize: 18),
        //readOnly: true,
        enabled: false,
        decoration: InputDecoration(
          border: const OutlineInputBorder(),
          labelText: labelString,
          prefixIcon: icon,
        ),
      ),
    ));
  }
}
