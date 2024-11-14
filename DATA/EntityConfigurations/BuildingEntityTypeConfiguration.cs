using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DATA.EntityConfigurations
{
    public class BuildingEntityTypeConfiguration : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.ToTable("Buildings");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Address).HasMaxLength(200).IsRequired();
            builder.Property(b => b.Name).HasMaxLength(200);
            builder.Property(b => b.Description).HasMaxLength(500);

            // Связь с Sensors (множество сенсоров у одного здания)
            builder.HasMany(b => b.Sensors)
                .WithOne(s => s.Building)
                .HasForeignKey(s => s.BuildingId);

            // Связь с User, добавляем внешний ключ для связи
            builder.HasOne(b => b.User) // Каждое здание связано с одним пользователем
                .WithMany(u => u.Buildings) // Один пользователь может иметь несколько зданий
                .HasForeignKey(b => b.UserId) // Указываем внешний ключ
                .OnDelete(DeleteBehavior.SetNull); // Поведение при удалении пользователя: оставляем здания без пользователя
        }
    }
}