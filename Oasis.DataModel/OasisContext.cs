using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oasis.DataModel.Model;

namespace Oasis.DataModel
{
    public class OasisContext : DbContext
    {
        public OasisContext() : base("name=Oasis")
        {
            
        }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }
        
        public DbSet<UserRole> UserRoles { get; set; }
        
        public DbSet<PermissionRole> PermissionRoles { get; set; }

        public DbSet<Permission> Permissions { get; set; }
    }
}
