using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace MonoTask.UI.WebApi.Auth
{
    public class CustomAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly CustomAuthenticationFunctionHandler _authenticationFunctionHandler;

        public CustomAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            TimeProvider clock,
            CustomAuthenticationFunctionHandler authenticationFunctionHandler)
            : base(options, logger, encoder)
        {
            _authenticationFunctionHandler = authenticationFunctionHandler;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var result = await _authenticationFunctionHandler.HandleAuthenticateAsync(Context);

            if (result == null || !result.Succeeded)
            {
                return AuthenticateResult.Fail("Authentication failed");
            }

            return result;
        }
    }
}
