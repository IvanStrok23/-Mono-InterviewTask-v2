import 'package:flutter/material.dart';
import 'package:mono_task_ui/features/login/widgets/helpers/login_widgets_enums.dart';

class InputFieldWidget extends StatefulWidget {
  const InputFieldWidget(
      {Key? key,
      required this.textType,
      required this.controller,
      this.textValue})
      : super(key: key);
  final String? textValue;
  final InputFieldWidgetType textType;
  final TextEditingController controller;

  @override
  State<InputFieldWidget> createState() => _InputFieldWidgetState();
}

class _InputFieldWidgetState extends State<InputFieldWidget> {
  bool _isObscure = false;

  @override
  void initState() {
    super.initState();
    // Call getUserData when the page is loaded
    _isObscure = widget.textType == InputFieldWidgetType.password;
  }

  String get labelString {
    String label;
    switch (widget.textType) {
      case InputFieldWidgetType.email:
        label = "Email";
        break;
      case InputFieldWidgetType.password:
        label = "Password";
        break;
      case InputFieldWidgetType.name:
        label = "Name";
        break;
    }
    return widget.textValue ?? label;
  }

  Icon get icon {
    switch (widget.textType) {
      case InputFieldWidgetType.email:
        return const Icon(Icons.email);
      case InputFieldWidgetType.password:
        return const Icon(Icons.lock);
      case InputFieldWidgetType.name:
        return const Icon(Icons.account_circle);
    }
  }

  IconButton? get suffixIconButton {
    if (widget.textType != InputFieldWidgetType.password) {
      return null;
    }
    return IconButton(
        icon: Icon(_isObscure ? Icons.visibility : Icons.visibility_off),
        onPressed: () {
          setState(() {
            _isObscure = !_isObscure;
          });
        });
  }

  @override
  Widget build(BuildContext context) {
    return Center(
        child: Padding(
      padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 16),
      child: TextField(
        obscureText:
            widget.textType == InputFieldWidgetType.password && _isObscure,
        textAlign: TextAlign.center,
        style: const TextStyle(fontSize: 18),
        controller: widget.controller,
        enabled: true,
        decoration: InputDecoration(
            border: const OutlineInputBorder(),
            labelText: labelString,
            prefixIcon: icon,
            suffixIcon: suffixIconButton),
      ),
    ));
  }
}
