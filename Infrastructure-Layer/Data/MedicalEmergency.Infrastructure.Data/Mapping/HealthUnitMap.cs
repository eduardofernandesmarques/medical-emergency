using MedicalEmergency.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace MedicalEmergency.Infrastructure.Data.Mapping
{
    public class HealthUnitMap : EntityTypeConfiguration<HealthUnit>
    {
        public HealthUnitMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.Name)
                .HasMaxLength(150);

            Property(t => t.Phone)
                .HasMaxLength(15);

            Property(t => t.Phone)
                .HasMaxLength(50);

            Property(t => t.Latitude)
                .HasMaxLength(25);

            Property(t => t.Longitude)
                .HasMaxLength(25);

            Property(t => t.LinkEN)
                .HasMaxLength(150);

            Property(t => t.LinkPT)
                .HasMaxLength(150);

            Property(t => t.LinkES)
                .HasMaxLength(150);

            Property(t => t.SpecialtiesEN)
                .IsOptional();

            Property(t => t.SpecialtiesES)
                .IsOptional();

            Property(t => t.SpecialtiesPT)
                .IsOptional();

            // Table & Column Mappings
            ToTable("HealthUnit");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Phone).HasColumnName("Phone");
            Property(t => t.Latitude).HasColumnName("Latitude");
            Property(t => t.Longitude).HasColumnName("Longitude");
            Property(t => t.LinkEN).HasColumnName("LinkEN");
            Property(t => t.LinkPT).HasColumnName("LinkPT");
            Property(t => t.LinkES).HasColumnName("LinkES");
        }
    }
}
