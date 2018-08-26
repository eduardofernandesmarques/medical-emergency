using MedicalEmergency.Domain.Entities.Types;
using System.Data.Entity.ModelConfiguration;

namespace MedicalEmergency.Infrastructure.Data.Mapping
{
    public class EmergencyTypeMap : EntityTypeConfiguration<EmergencyType>
    {
        public EmergencyTypeMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("EmergencyType");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Description).HasColumnName("Description");
        }
    }
}
