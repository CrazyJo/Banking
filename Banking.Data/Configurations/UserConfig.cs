using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Banking.Model;

namespace Banking.Data.Configurations
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            Property(p => p.Login)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("IX_Users_Login", 1) {IsUnique = true}));
        }
    }
}
