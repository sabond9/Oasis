using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oasis.DataModel.Model;

namespace Oasis.DataModel.Configurations
{
    public class PermisssionEntityConfiguration : EntityTypeConfiguration<Permission>
    {
        public PermisssionEntityConfiguration()
        {
            Property(r => r.Name).IsRequired().HasMaxLength(100);
        }
    }
}
