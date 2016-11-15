using System.Web.Helpers;
using Oasis.DataModel.Model;

namespace Oasis.DataModel.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Oasis.DataModel.OasisContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Oasis.DataModel.OasisContext context)
        {
            //var user = new User
            //{
            //    UserName = "Admin",
            //    PasswordHash = Crypto.HashPassword("12345")
            //};
            //context.Users.Add(user);

            //var permission = new Permission
            //{
            //    Name = "AdminPermission"
            //};

            //context.Permissions.Add(permission);

            //var role = new Role
            //{
            //    Name = "Admin"
            //};
            //context.Roles.Add(role);

            //var permissionRole = new PermissionRole
            //{
            //    Permission = permission,
            //    Role = role
            //};

            //context.PermissionRoles.Add(permissionRole);

            //permission.PermissionRoles.Add(permissionRole);
            //role.PermissionRoles.Add(permissionRole);

        }
    }
}
