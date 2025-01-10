using Microsoft.EntityFrameworkCore;
using parc.Models;
using parc.Models.shared;

namespace parc.Services;

public class ParcService
{
    private readonly AppDbContext _context;

    public ParcService(AppDbContext context)
    {
        _context = context;
    }
    public Parc Add(Parc parcDto, int userId)
    {
        parcDto.OwnerId = userId;
        _context.Parcs.Add(parcDto);
        _context.SaveChanges();
        return parcDto;
    }

    public List<Parc> GetAll(CustomUser user)
    {
        if(user.Role == UserRole.SuperAdmin)
            return _context.Parcs.AsNoTracking().Include(s => s.Salles).ToList();
        return _context.Parcs.Where(p => p.OwnerId == user.Id && p.IsActive).ToList();
    }

    public Parc GetById(int id, CustomUser user)
    {
        if(user.Role == UserRole.SuperAdmin)
            return _context.Parcs.AsNoTracking().SingleOrDefault(x => x.Id == id);
        return _context.Parcs.Where(p => p.OwnerId == user.Id && p.Id == id && p.IsActive).Include(p => p.Salles).FirstOrDefault();
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