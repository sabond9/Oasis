using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Oasis.Common.Enums;
using Oasis.DataModel.Enums;

namespace Oasis.Common
{
    public static class AppSecurityContext
    {
        private static ClaimsIdentity ClaimsIdentity => (ClaimsIdentity)HttpContext.Current.User.Identity;

        public static int? UserId
        {
            get
            {
                if (ClaimsIdentity == null) return null;

                int userId;
                var userIdClaim = ClaimsIdentity.FindFirst(cl => cl.Type == ClaimType.NameIdentifier);

                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out userId))
                    return userId;

                return null;
            }
        }

        public static bool IsUserAuthenticated => ClaimsIdentity.IsAuthenticated;

        public static string UserName
        {
            get
            {
                var userNameClaim = ClaimsIdentity?.FindFirst(cl => cl.Type == ClaimType.Name);

                return userNameClaim?.Value;
            }
        }

        public static ICollection<Permissions> UserPermissions
        {
            get
            {
                if (ClaimsIdentity == null) return null;

                var userDataClaim = ClaimsIdentity.FindFirst(cl => cl.Type == ClaimType.Permission);

                if (userDataClaim == null) return new List<Permissions>();

                var userPermissionIds = userDataClaim.Value
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .Cast<Permissions>()
                    .ToList();

                return userPermissionIds;
            }
        }

        public static bool UserHasPermissions(IEnumerable<Permissions> permissions)
        {
            if (permissions == null || !permissions.Any() || UserPermissions == null) return true;
            return UserPermissions.Any(permissions.Contains);
        }

        public static bool UserHasPermission(Permissions permission)
        {
            if ((int)permission == 0 || UserPermissions == null) return true;
            return UserPermissions.Contains(permission);
        }
    }
}