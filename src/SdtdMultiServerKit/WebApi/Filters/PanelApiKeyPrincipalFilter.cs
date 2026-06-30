using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using SdtdMultiServerKit.WebApi.Middlewares;

namespace SdtdMultiServerKit.WebApi.Filters
{
    /// <summary>
    /// Copies the OWIN principal set by <see cref="PanelApiKeyAuthenticationMiddleware"/>
    /// into the Web API request context. HostAuthenticationFilter does not work here because
    /// it calls AuthenticateAsync on a scheme with no registered OWIN auth handler.
    /// </summary>
    public class PanelApiKeyPrincipalFilter : IAuthorizationFilter
    {
        public bool AllowMultiple => false;

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(
            HttpActionContext actionContext,
            CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            var owinContext = actionContext.Request.GetOwinContext();
            var user = owinContext?.Authentication?.User;
            if (user?.Identity?.IsAuthenticated == true
                && user.Identity.AuthenticationType == PanelApiKeyAuthenticationMiddleware.AuthenticationType)
            {
                actionContext.RequestContext.Principal = user;
            }

            return continuation();
        }
    }
}
