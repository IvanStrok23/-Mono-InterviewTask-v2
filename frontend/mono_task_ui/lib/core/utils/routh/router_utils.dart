enum APP_PAGE { home, login, profile }

extension AppPageExtension on APP_PAGE {
  // create path for routes
  String get routePath {
    switch (this) {
      case APP_PAGE.home:
        return "/";

      case APP_PAGE.login:
        return "/login";

      case APP_PAGE.profile:
        return "/profile";

      default:
        return "/";
    }
  }

  String get routeName {
    switch (this) {
      case APP_PAGE.home:
        return "home";

      case APP_PAGE.login:
        return "login";

      case APP_PAGE.profile:
        return "profile";

      default:
        return "home";
    }
  }
}
