using Microsoft.EntityFrameworkCore;
using parc.Models;

namespace parc;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}
    public DbSet<Parc> Parcs { get; set; }
    public DbSet<Salle> Salles { get; set; }
}