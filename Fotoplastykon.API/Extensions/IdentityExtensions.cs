using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Extensions
{
    public static class IdentityExtensions
    {
        #region Id()
        public static long Id(this IPrincipal principal)
        {
            return Convert.ToInt64((principal as ClaimsPrincipal).FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        public static long Id(this IIdentity identity)
        {
            return Convert.ToInt64((identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
        #endregion

        #region IsAdmin()
        public static bool IsAdmin(this IIdentity identity)
        {
            return Convert.ToBoolean((identity as ClaimsIdentity).FindFirst("IsAdmin")?.Value);
        }
        #endregion

        #region IsAdmin()
        public static string CanEditPages(this IIdentity identity)
        {
            return (identity as ClaimsIdentity).FindFirst("CanEditPages")?.Value;
        }
        #endregion
    }
}
