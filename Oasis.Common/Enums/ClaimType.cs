using System.Security.Claims;
using System.Security.Policy;

namespace Oasis.Common.Enums
{
    public static class ClaimType
    {
        public static string NameIdentifier = ClaimTypes.NameIdentifier;

        public static string Role = ClaimTypes.Role;

        public static string Name = ClaimTypes.Name;

        public static string Permission = "Permission";
    }
}
