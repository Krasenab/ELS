using System.Security.Claims;

namespace ELS.Infrastrucure
{
    public static class ClaimsPrincipalExtensions
    {
        public static string getCurrentUserId(ClaimsPrincipal user) 
        {
            string result = user.FindFirst(ClaimTypes.NameIdentifier).Value;

            return result;
        }
        public static bool IsAutchenticated(ClaimsPrincipal user) 
        {
            return user.Identity.IsAuthenticated;
           
        }
    }
}
