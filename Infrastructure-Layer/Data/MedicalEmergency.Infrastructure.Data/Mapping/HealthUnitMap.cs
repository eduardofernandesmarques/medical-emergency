using MedicalEmergency.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace MedicalEmergency.Infrastructure.Data.Mapping
{
    public class HealthUnitMap : EntityTypeConfiguration<HealthUnit>
    {
        public HealthUnitMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(150);

            this.Property(t => t.Phone)
                .HasMaxLength(15);

            this.Property(t => t.Phone)
                .HasMaxLength(50);

            this.Property(t => t.Latitude)
                .HasMaxLength(20);

            this.Property(t => t.Longitude)
                .HasMaxLength(20);

            this.Property(t => t.LinkEN)
                .HasMaxLength(150);

            this.Property(t => t.LinkPT)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("HealthUnit");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.LinkEN).HasColumnName("LinkEN");
            this.Property(t => t.LinkPT).HasColumnName("LinkPT");
        }
    }
}
