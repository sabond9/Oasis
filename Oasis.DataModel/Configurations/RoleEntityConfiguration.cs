using System.Data.Entity.ModelConfiguration;
using Oasis.DataModel.Model;

namespace Oasis.DataModel.Configurations
{
    public class RoleEntityConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleEntityConfiguration()
        {
            Property(r => r.Name).IsRequired().HasMaxLength(100);
        }
    }
}
