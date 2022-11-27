using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType) //bir User'in Claim'lerine catmaq ucun .Net'de olan class
        {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList(); //? isaresi null ola bileceyini bildirir/ bir claim ola bile ki token istemir
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal) //User ola biler bir basa butun rollari istesin.
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }
    }
}
