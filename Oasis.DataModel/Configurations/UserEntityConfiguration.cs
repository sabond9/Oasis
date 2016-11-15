using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Oasis.DataModel.Model;

namespace Oasis.DataModel.Configurations
{
    public class UserEntityConfiguration : EntityTypeConfiguration<User>
    {
        public UserEntityConfiguration()
        {
            Property(r => r.UserName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnAnnotation("IX_Username", new IndexAnnotation(new IndexAttribute() {IsUnique = true}));

            Property(r => r.PasswordHash).IsRequired().HasMaxLength(100);
        }
    }
}
