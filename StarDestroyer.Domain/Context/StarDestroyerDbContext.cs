using Microsoft.EntityFrameworkCore;

namespace StarDestroyer.Domain;

public class StarDestroyerDbContext : DbContext
{
    public StarDestroyerDbContext(DbContextOptions<StarDestroyerDbContext> options) : base(options)
    {
    }
    
    public DbSet<Starship> Starships => Set<Starship>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Starship>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Model).HasMaxLength(100);
            entity.Property(e => e.Manufacturer).HasMaxLength(100);
            entity.Property(e => e.CostInCredits).HasMaxLength(50);
            entity.Property(e => e.Length).HasMaxLength(50);
            entity.Property(e => e.MaxAtmospheringSpeed).HasMaxLength(50);
            entity.Property(e => e.Crew).HasMaxLength(50);
            entity.Property(e => e.Passengers).HasMaxLength(50);
            entity.Property(e => e.CargoCapacity).HasMaxLength(50);
            entity.Property(e => e.Consumables).HasMaxLength(50);
            entity.Property(e => e.HyperdriveRating).HasMaxLength(50);
            entity.Property(e => e.MGLT).HasMaxLength(50);
            entity.Property(e => e.StarshipClass).HasMaxLength(50);
            entity.Property(e => e.Url).HasMaxLength(500);
        });
    }
}
