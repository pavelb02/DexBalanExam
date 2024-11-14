using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DATA.EntityConfigurations
{
    public class SensorEntityTypeConfiguration : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            builder.ToTable("Sensors");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.Term)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(s => s.Charge)
                .IsRequired();

            builder.Property(s => s.Water)
                .IsRequired();

            builder.Property(s => s.ImagePath)
                .HasMaxLength(255); // Указываем максимально допустимую длину для пути к изображению

            // Настройка координат как собственные свойства в таблице Sensors
            builder.OwnsOne(s => s.Coordinate, coord =>
            {
                coord.Property(c => c.X).HasColumnName("CoordinateX").IsRequired();
                coord.Property(c => c.Y).HasColumnName("CoordinateY").IsRequired();
            });

            // Связь с Building
            builder.HasOne(s => s.Building)
                .WithMany(b => b.Sensors)
                .HasForeignKey(s => s.BuildingId);
        }
    }
}