using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SdtdMultiServerKit.WebApi.Filters
{
    /// <summary>
    /// ApiOnly auth: validate X-Api-Key on the Web API request itself (not via OWIN principal),
    /// then set RequestContext.Principal before [Authorize] runs.
    /// </summary>
    public class PanelApiKeyAuthorizationFilter : IAuthorizationFilter
    {
        public bool AllowMultiple => false;

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(
            HttpActionContext actionContext,
            CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            if (!ModApi.AppSettings.ApiOnly || !RequiresAuthorization(actionContext))
            {
                return continuation();
            }

            string? apiKey = PanelApiKeyAuthenticator.ReadApiKey(actionContext.Request.Headers);
            if (PanelApiKeyAuthenticator.IsValidApiKey(apiKey))
            {
                actionContext.RequestContext.Principal = PanelApiKeyAuthenticator.CreatePrincipal();
                return continuation();
            }

            return Task.FromResult(
                actionContext.Request.CreateResponse(
                    HttpStatusCode.Unauthorized,
                    new { message = "Authorization has been denied for this request." }));
        }

        private static bool RequiresAuthorization(HttpActionContext actionContext)
        {
            var controllerDescriptor = actionContext.ControllerContext.ControllerDescriptor;

            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                || controllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return false;
            }

            return actionContext.ActionDescriptor.GetCustomAttributes<AuthorizeAttribute>().Any()
                || controllerDescriptor.GetCustomAttributes<AuthorizeAttribute>().Any();
        }
    }
}
