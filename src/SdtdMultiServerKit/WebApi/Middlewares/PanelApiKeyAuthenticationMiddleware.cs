using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using SdtdMultiServerKit.WebApi;

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
            string? apiKey = PanelApiKeyAuthenticator.ReadApiKey(context.Request.Headers);

            if (PanelApiKeyAuthenticator.IsValidApiKey(apiKey))
            {
                var principal = PanelApiKeyAuthenticator.CreatePrincipal();
                context.Authentication.SignIn(new AuthenticationProperties(), (ClaimsIdentity)principal.Identity!);
                context.Authentication.User = principal;
                context.Request.User = principal;
            }

            return Next.Invoke(context);
        }
    }
}
