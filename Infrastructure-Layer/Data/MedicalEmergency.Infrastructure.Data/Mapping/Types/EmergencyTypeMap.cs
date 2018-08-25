using MedicalEmergency.Domain.Entities.Types;
using System.Data.Entity.ModelConfiguration;

namespace MedicalEmergency.Infrastructure.Data.Mapping
{
    public class EmergencyTypeMap : EntityTypeConfiguration<EmergencyType>
    {
        public EmergencyTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("EmergencyType");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
