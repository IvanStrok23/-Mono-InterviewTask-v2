using MonoTask.Infrastructure.Data.Entities;

namespace MonoTask.UI.WebApi.Auth.UserPrincipal
{
    public class UserPrincipal : IUserPrincipal
    {
        public UserEntity User { get; private set; }

        public bool IsAdmin => User.IsAdmin;
        public bool IsClient => User.IsClient;
        public bool IsSuperAdmin => User.IsSuperAdmin;

        void IUserPrincipal.SetUser(UserEntity user)
        {
            if (User != null)
            {
                throw new InvalidOperationException("User has already been set and cannot be modified.");
            }

            User = user ?? throw new ArgumentNullException(nameof(user));
        }
    }
}
