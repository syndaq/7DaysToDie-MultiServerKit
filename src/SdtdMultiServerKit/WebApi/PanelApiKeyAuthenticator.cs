using System.Net.Http.Headers;
using System.Security.Claims;
using SdtdMultiServerKit.WebApi.Middlewares;

namespace SdtdMultiServerKit.WebApi
{
    internal static class PanelApiKeyAuthenticator
    {
        internal const int AuthVersion = 3;

        internal static string? ReadApiKey(HttpRequestHeaders headers)
        {
            if (headers.TryGetValues(PanelApiKeyAuthenticationMiddleware.ApiKeyHeaderName, out IEnumerable<string>? values))
            {
                string? headerValue = values.FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(headerValue))
                {
                    return headerValue.Trim();
                }
            }

            if (headers.Authorization != null
                && string.Equals(headers.Authorization.Scheme, "Bearer", StringComparison.OrdinalIgnoreCase)
                && !string.IsNullOrWhiteSpace(headers.Authorization.Parameter))
            {
                return headers.Authorization.Parameter.Trim();
            }

            return null;
        }

        internal static string? ReadApiKey(IDictionary<string, string[]> headers)
        {
            foreach (var pair in headers)
            {
                if (string.Equals(pair.Key, PanelApiKeyAuthenticationMiddleware.ApiKeyHeaderName, StringComparison.OrdinalIgnoreCase))
                {
                    string? headerValue = pair.Value.FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(headerValue))
                    {
                        return headerValue.Trim();
                    }
                }
            }

            if (headers.TryGetValue("Authorization", out string[]? authValues))
            {
                string? authHeader = authValues.FirstOrDefault();
                if (!string.IsNullOrEmpty(authHeader)
                    && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    return authHeader.Substring("Bearer ".Length).Trim();
                }
            }

            return null;
        }

        internal static bool IsValidApiKey(string? apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                return false;
            }

            string expectedKey = ModApi.AppSettings.PanelApiKey;
            return !string.IsNullOrEmpty(expectedKey)
                && string.Equals(apiKey.Trim(), expectedKey, StringComparison.Ordinal);
        }

        internal static ClaimsPrincipal CreatePrincipal()
        {
            var identity = new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.Name, "panel"),
                    new Claim(ClaimTypes.Role, "panel"),
                },
                PanelApiKeyAuthenticationMiddleware.AuthenticationType);

            return new ClaimsPrincipal(identity);
        }

        internal static string FormatKeyPrefix(string key)
        {
            if (key.Length <= 12)
            {
                return key;
            }

            return key.Substring(0, 12) + "…";
        }
    }
}
