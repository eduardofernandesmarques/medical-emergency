using MedicalEmergency.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace MedicalEmergency.Infrastructure.Data.Mapping
{
    public class HealthUnitSpecialityMap : EntityTypeConfiguration<HealthUnitSpeciality>
    {
        public HealthUnitSpecialityMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            this.Property(t => t.HealthUnitID)
                .IsRequired();

            this.Property(t => t.SpecialityID)
                .IsRequired();

            this.HasRequired(t => t.HealthUnit).WithMany(p => p.SpecialtiesEN).HasForeignKey(t => t.HealthUnitID);

            this.HasRequired(t => t.HealthUnit).WithMany(p => p.SpecialtiesES).HasForeignKey(t => t.HealthUnitID);

            this.HasRequired(t => t.HealthUnit).WithMany(p => p.SpecialtiesPT).HasForeignKey(t => t.HealthUnitID);

            this.HasRequired(t => t.Speciality).WithMany(p => p.HealthUnitSpeciality).HasForeignKey(t => t.SpecialityID);

            // Table & Column Mappings
            this.ToTable("HealthUnitSpeciality");
        }
    }
}
