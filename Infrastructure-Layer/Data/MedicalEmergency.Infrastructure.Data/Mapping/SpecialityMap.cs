using MedicalEmergency.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace MedicalEmergency.Infrastructure.Data.Mapping
{
    public class SpecialityMap : EntityTypeConfiguration<Specialty>
    {
        public SpecialityMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Speciality");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}