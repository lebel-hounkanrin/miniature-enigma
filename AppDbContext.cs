using Microsoft.EntityFrameworkCore;
using parc.Models;

namespace parc;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}
    public DbSet<Parc> Parcs { get; set; }
    public DbSet<Salle> Salles { get; set; }
    public DbSet<Device> Devices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // modelBuilder.Entity<Device>().Property(p => p.Status).HasConversion<string>();
        // modelBuilder.Entity<Device>().Property(p => p.Type).HasConversion<string>();
        modelBuilder.Entity<Salle>().HasOne(s => s.Parc).WithMany(p => p.Salles).HasForeignKey(s => s.ParcId);
        modelBuilder.Entity<Device>().HasOne(d => d.Salle).WithMany(s => s.Devices).HasForeignKey(d => d.SalleId);
    }
}