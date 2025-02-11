using Microsoft.AspNetCore.Authorization;
using MonoTask.Infrastructure.Data.Entities;

namespace MonoTask.UI.WebApi.Auth.Handlers;

public class IsClientHandler : AuthorizationHandler<IsClientRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IsClientHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsClientRequirement requirement)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext != null && httpContext.Items.ContainsKey("UserInfo"))
        {
            var userInfo = httpContext.Items["UserInfo"] as UserEntity;

            if (userInfo != null && userInfo.IsClient)
            {
                context.Succeed(requirement);
            }
        }

        return Task.CompletedTask;
    }
}
