using MedicalEmergency.Domain.Entities.Manager;
using System.Data.Entity.ModelConfiguration;

namespace MedicalEmergency.Infrastructure.Data.Mapping.Manager
{
    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            this.Property(t => t.Login)
              .IsRequired()
              .HasMaxLength(25);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Account", "Manager");
        }
    }
}