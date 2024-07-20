using Microsoft.EntityFrameworkCore;

namespace NaturalGasApp.EfStructures;

public class NaturalGasDbContext : DbContext
{
    public DbSet<NaturalGasConsumption> NaturalGasConsumptions => Set<NaturalGasConsumption>();
    
    public NaturalGasDbContext(DbContextOptions<NaturalGasDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NaturalGasConsumption>()
            .Property(n => n.AmountToPay)
            .HasConversion<double>();
    }
}