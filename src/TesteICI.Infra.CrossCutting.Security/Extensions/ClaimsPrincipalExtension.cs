using System.Security.Claims;

namespace TesteICI.Infra.CrossCutting.Security.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string? GetUserId(this ClaimsPrincipal principal)
        {
            if (principal is null)
                throw new ArgumentException(nameof(principal));

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        public static string? GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal is null)
                throw new ArgumentException(nameof(principal));

            var claim = principal.FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }
    }
}
