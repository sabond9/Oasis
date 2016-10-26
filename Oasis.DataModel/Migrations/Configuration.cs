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
            var user = new User();
            user.UserName = "Admin";
            user.PasswordHash = "12345";
            context.Users.Add(user);
        }
    }
}
