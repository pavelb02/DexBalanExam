using DATA.EntityConfigurations;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data;

public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Sensor> Sensors { get; set; }
    public DbSet<Building> Buildings { get; set; }

    /*public MyDbContext()
    {
        Database.EnsureCreated();
    }*/

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        optionsBuilder.UseNpgsql("Host=localhost; Port=5432; Database=ExamDexDB; Username=postgres; Password=postgres");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder == null) throw new ArgumentException(nameof(modelBuilder));
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BuildingEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new SensorEntityTypeConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}