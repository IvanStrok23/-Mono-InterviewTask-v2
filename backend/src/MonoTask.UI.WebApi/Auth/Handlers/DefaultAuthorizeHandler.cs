using Microsoft.AspNetCore.Authorization;
using MonoTask.Infrastructure.Data.Entities;

namespace MonoTask.UI.WebApi.Auth.Handlers
{
    public class DefaultAuthorizeHandler : AuthorizationHandler<DefaultUserRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DefaultAuthorizeHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DefaultUserRequirement requirement)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null && httpContext.Items.ContainsKey("UserInfo"))
            {
                var userInfo = httpContext.Items["UserInfo"] as UserEntity;

                if (userInfo != null && (userInfo.Roles != 0))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }

}
