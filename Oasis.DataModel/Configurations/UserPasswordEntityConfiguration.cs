using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oasis.DataModel.Model;

namespace Oasis.DataModel.Configurations
{
    public class UserPasswordEntityConfiguration : EntityTypeConfiguration<UserPassword>
    {
        public UserPasswordEntityConfiguration()
        {
            Property(r => r.PasswordHash).IsRequired().HasMaxLength(100);
            Property(r => r.PasswordCreatedDate).IsRequired();
        }
    }
}
