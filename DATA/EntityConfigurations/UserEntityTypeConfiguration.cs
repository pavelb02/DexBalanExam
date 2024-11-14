using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DATA.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name).HasMaxLength(100).IsRequired();

            // Связь с Buildings (множество зданий у одного пользователя)
            builder.HasMany(u => u.Buildings)
                .WithOne(b => b.User)  // Каждое здание связано с одним пользователем
                .HasForeignKey(b => b.UserId)  // Указываем внешний ключ
                .OnDelete(DeleteBehavior.SetNull); // Поведение при удалении пользователя: оставляем здания без пользователя
        }
    }
}