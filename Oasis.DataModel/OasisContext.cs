using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oasis.DataModel.Configurations;
using Oasis.DataModel.Model;

namespace Oasis.DataModel
{
    public class OasisContext : DbContext
    {
        public OasisContext() : base("name=Oasis")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }
        
        public DbSet<UserRole> UserRoles { get; set; }
        
        public DbSet<PermissionRole> PermissionRoles { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<UserPassword> UserPasswords { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserEntityConfiguration());
            modelBuilder.Configurations.Add(new RoleEntityConfiguration());
            modelBuilder.Configurations.Add(new PermisssionEntityConfiguration());
            modelBuilder.Configurations.Add(new UserPasswordEntityConfiguration());
        }
    }
}
