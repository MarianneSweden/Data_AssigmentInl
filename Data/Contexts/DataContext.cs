using Microsoft.EntityFrameworkCore;
using Data.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        //Ensure that ProductEntity.Price has the correct data type in the database

        modelBuilder.Entity<ProductEntity>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<ProjectEntity>()
            .Property(p => p.PricePerHour)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<ProjectEntity>()
            .HasIndex(p => p.ProjectNumber)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }

    public virtual DbSet<CustomerEntity> Customers { get; set; } = null!;
    public virtual DbSet<ProductEntity> Products { get; set; } = null!;
    public virtual DbSet<StatusTypeEntity> StatusTypes { get; set; } = null!;
    public virtual DbSet<UserEntity> Users { get; set; } = null!;
    public virtual DbSet<ProjectEntity> Projects { get; set; } = null!;
}
