using Microsoft.EntityFrameworkCore;
using parc.Models;

namespace parc.Services;

public class ParcService
{
    private readonly AppDbContext _context;

    public ParcService(AppDbContext context)
    {
        _context = context;
    }
    public Parc Add(Parc parcDto)
    {
        _context.Parcs.Add(parcDto);
        _context.SaveChanges();
        return parcDto;
    }

    public List<Parc> GetAll()
    {
        return _context.Parcs.AsNoTracking().Include(s => s.Salles).ToList();
    }

    public Parc GetById(int id)
    {
        return _context.Parcs.AsNoTracking().SingleOrDefault(x => x.Id == id);
    }

    public static Parc Update(int id, Parc parcDto)
    {
        return new Parc();
    }

    public static bool Delete(int id)
    {
        return true;
    }
}