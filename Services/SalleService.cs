using Microsoft.EntityFrameworkCore;
using parc.Models;

namespace parc.Services;

public class SalleService
{
    private readonly AppDbContext _context;

    public SalleService(AppDbContext context)
    {
        _context = context;
    }
    public Salle Add(Salle salle)
    {
        _context.Salles.Add(salle);
        _context.SaveChanges();
        return salle;
    }
    
    public List<Salle> GetAll()
    {
        return _context.Salles.AsNoTracking().ToList();
    }
}