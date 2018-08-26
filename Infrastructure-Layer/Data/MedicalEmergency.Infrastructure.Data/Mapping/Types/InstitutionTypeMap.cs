using MedicalEmergency.Domain.Entities.Types;
using System.Data.Entity.ModelConfiguration;

namespace MedicalEmergency.Infrastructure.Data.Mapping
{
    public class InstitutionTypeMap : EntityTypeConfiguration<InstitutionType>
    {
        public InstitutionTypeMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("InstitutionType");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Description).HasColumnName("Description");
        }
    }
}
