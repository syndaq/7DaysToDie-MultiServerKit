using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;

namespace SdtdMultiServerKit.WebApi.Middlewares
{
    /// <summary>
    /// Authenticates requests from the central panel using a shared API key.
    /// </summary>
    public class PanelApiKeyAuthenticationMiddleware : OwinMiddleware
    {
        public const string AuthenticationType = "PanelApiKey";
        public const string ApiKeyHeaderName = "X-Api-Key";

        public PanelApiKeyAuthenticationMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            string? apiKey = context.Request.Headers.Get(ApiKeyHeaderName);
            if (string.IsNullOrEmpty(apiKey))
            {
                apiKey = GetBearerToken(context.Request.Headers.Get("Authorization"));
            }

            if (IsValidApiKey(apiKey))
            {
                var identity = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, "panel"),
                        new Claim(ClaimTypes.Role, "panel"),
                    },
                    AuthenticationType);

                var principal = new ClaimsPrincipal(identity);
                context.Authentication.SignIn(new AuthenticationProperties(), identity);
                context.Authentication.User = principal;
                context.Request.User = principal;
            }

            return Next.Invoke(context);
        }

        private static bool IsValidApiKey(string? apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                return false;
            }

            string expectedKey = ModApi.AppSettings.PanelApiKey;
            return !string.IsNullOrEmpty(expectedKey)
                && string.Equals(apiKey.Trim(), expectedKey, StringComparison.Ordinal);
        }

        private static string? GetBearerToken(string? authorizationHeader)
        {
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return null;
            }

            const string bearerPrefix = "Bearer ";
            string header = authorizationHeader!;
            if (header.StartsWith(bearerPrefix, StringComparison.OrdinalIgnoreCase))
            {
                return header.Substring(bearerPrefix.Length).Trim();
            }

            return null;
        }
    }
}
