using System.Security.Claims;

namespace LibApp.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            var userId = principal.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            return userId;
        }

        public static string GetUserRole(this ClaimsPrincipal principal)
        {
            var userId = principal.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;

            return userId;
        }
    }
}