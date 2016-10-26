using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oasis.DataModel.Model
{
    public class Permission
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<PermissionRole> PermissionRoles { get; set; } = new HashSet<PermissionRole>();
    }
}
