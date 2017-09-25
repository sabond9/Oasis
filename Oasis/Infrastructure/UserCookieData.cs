using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oasis.Infrastructure
{
    public class UserCookieData
    {
        public int UserId { get; set; }

        public HashSet<int> PermissionIds { get; set; } = new HashSet<int>();

        public HashSet<string> Roles { get; set; } = new HashSet<string>();

        public string UserName { get; set; }
    }
}