using System.Linq;
using System.Security.Principal;
using System.Web.Security;
using WebMatrix.WebData;

namespace CaucasianPearl.Core.Helpers.HtmlHelpers
{
    public static class PrincipalHtmlHelper
    {
        public static bool IsInAnyRole(this IPrincipal user, string roles)
        {
            return WebSecurity.IsAuthenticated &&
                   Roles.GetRolesForUser(user.Identity.Name).Any(roles.Split(',').Contains);
        }
    }
}