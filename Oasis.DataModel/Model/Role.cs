using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oasis.DataModel.Model
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();

        public ICollection<PermissionRole> PermissionRoles { get; set; } = new HashSet<PermissionRole>();
    }
}
