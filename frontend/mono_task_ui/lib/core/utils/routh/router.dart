import 'package:go_router/go_router.dart';
import 'package:mono_task_ui/core/auth/auth_listener.dart';
import 'package:mono_task_ui/core/auth/custom_auth.dart';
import 'package:mono_task_ui/core/utils/routh/router_utils.dart';
import 'package:mono_task_ui/features/home/home_page.dart';
import 'package:mono_task_ui/features/login/login_page.dart';
import 'package:mono_task_ui/features/profile/profile_page.dart';

GoRouter createRouter(CustomAuth auth) {
  return GoRouter(
    refreshListenable: AuthListener(auth),
    routes: [
      GoRoute(
        path: APP_PAGE.home.routePath,
        name: APP_PAGE.home.routeName,
        builder: (context, state) => HomePage(),
        routes: [
          GoRoute(
            path: APP_PAGE.profile.routePath,
            name: APP_PAGE.profile.routeName,
            builder: (context, state) {
              final userId =
                  int.tryParse(state.uri.queryParameters['userId'] ?? '') ?? 0;
              return ProfilePage(userId: userId);
            },
          ),
          GoRoute(
            path: APP_PAGE.login.routePath,
            name: APP_PAGE.login.routeName,
            builder: (context, state) => const LoginPage(),
          ),
        ],
      )
    ],
    redirect: (context, state) {
      final isLoggedIn = auth.currentUser != null;
      final isOnProfilePage =
          state.matchedLocation == APP_PAGE.profile.routePath;
      if (!isLoggedIn && isOnProfilePage) {
        return APP_PAGE.login.routePath;
      }
      final isOnLoginPage = state.matchedLocation == APP_PAGE.login.routePath;
      print('1: $isOnLoginPage');
      print('2: $isLoggedIn');

      if (isLoggedIn && isOnLoginPage) {
        return APP_PAGE.profile.routePath;
      }
      return null;
    },
  );
}
