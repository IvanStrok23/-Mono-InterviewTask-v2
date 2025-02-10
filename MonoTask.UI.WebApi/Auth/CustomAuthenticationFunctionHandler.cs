using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MonoTask.Infrastructure.Data.Entities;
using MonoTask.Infrastructure.Data.Interfaces;
using MonoTask.UI.WebApi.Auth.UserPrincipal;
using System.Security.Claims;

namespace MonoTask.UI.WebApi.Auth
{
    public class CustomAuthenticationFunctionHandler
    {
        private const string BEARER_PREFIX = "Bearer ";

        private readonly IDataContext _context;
        private IUserPrincipal _userPrincipal;

        public CustomAuthenticationFunctionHandler(IDataContext context, IUserPrincipal userPrincipal)
        {
            _context = context;
            _userPrincipal = userPrincipal;
        }
        public Task<AuthenticateResult> HandleAuthenticateAsync(HttpRequest request) =>
                HandleAuthenticateAsync(request.HttpContext);


        public async Task<AuthenticateResult> HandleAuthenticateAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.NoResult();
            }

            string? bearerToken = context.Request.Headers["Authorization"];

            if (bearerToken == null || !bearerToken.StartsWith(BEARER_PREFIX))
            {
                return AuthenticateResult.Fail("Invalid scheme.");
            }

            string token = bearerToken.Substring(BEARER_PREFIX.Length);

            try
            {
                var userInfo = await ValidateTokenAndExtractUserInfoAsync(token);

                if (userInfo == null)
                {
                    return AuthenticateResult.Fail("Invalid token.");
                }

                var ticket = new AuthenticationTicket(new ClaimsPrincipal(), JwtBearerDefaults.AuthenticationScheme);
                context.Items["UserInfo"] = userInfo;
                _userPrincipal.SetUser(userInfo);
                return AuthenticateResult.Success(ticket);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex);
            }
        }

        private async Task<UserEntity?> ValidateTokenAndExtractUserInfoAsync(string token)
        {
            // we can simulate token validation here
            return await _context.Users.FirstOrDefaultAsync(u => u.Token == token);
        }

    }


}