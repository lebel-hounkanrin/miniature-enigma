using Microsoft.EntityFrameworkCore;
using parc.Dto;
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

    public Parc Update(int id, UpdateParcDto? parcDto, CustomUser user)
    {
        if (user.Role == UserRole.SuperAdmin)
        {
            var _parc =  _context.Parcs.AsNoTracking().SingleOrDefault(x => x.Id == id);
            _parc.Name = parcDto.Name ?? _parc.Name;
            _parc.Location = parcDto.Location ?? _parc.Location;
            _context.Parcs.Update(_parc);
            return _parc;
        }
        var parc = _context.Parcs.Where(p => p.OwnerId == user.Id && p.Id == id && p.IsActive).Include(p => p.Salles).FirstOrDefault();
        if (parc != null)
        {
            if (parcDto.Name != null) parc.Name = parcDto.Name;
            if(parcDto.Location != null)  parc.Location = parcDto.Location;
            _context.Parcs.Update(parc);
            // _context.SaveChanges();
            return parc;
        }
        return null;
    }

    public bool Delete(int id, CustomUser user)
    {
        try
        {
            if (user.Role == UserRole.SuperAdmin)
            {
                var _parc =  _context.Parcs.AsNoTracking().SingleOrDefault(x => x.Id == id);
                _context.Parcs.Remove(_parc);
                _context.SaveChangesAsync();
                return true;

            }
            var parc = _context.Parcs.Where(p => p.OwnerId == user.Id && p.Id == id && p.IsActive).Include(p => p.Salles).FirstOrDefault();
            _context.Parcs.Remove(parc);
            _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}