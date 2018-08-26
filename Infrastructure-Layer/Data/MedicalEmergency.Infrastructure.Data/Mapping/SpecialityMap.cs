using MedicalEmergency.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace MedicalEmergency.Infrastructure.Data.Mapping
{
    public class SpecialityMap : EntityTypeConfiguration<Specialty>
    {
        public SpecialityMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.Description)
                .HasMaxLength(50);

            Property(t => t.Language)
                .HasMaxLength(2);

            // Table & Column Mappings
            ToTable("Speciality");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Description).HasColumnName("Description");
            Property(t => t.Language).HasColumnName("Language");
        }
    }
}