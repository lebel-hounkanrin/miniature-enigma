using Microsoft.EntityFrameworkCore;
using parc.Models;
using parc.Models.shared;

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
    
    public Salle GetById(int id)
    {
        // if(user.Role == UserRole.SuperAdmin)
        //     return _context.Salles.AsNoTracking().SingleOrDefault(x => x.Id == id);
        return _context.Salles.Where(s => s.Id == id).Include(s => s.Devices).FirstOrDefault();
    }

    public async Task<bool> Update(int id, Salle updateModel)
    {
        var salle = await _context.Salles.FirstOrDefaultAsync(s => s.Id == id);
        if (salle == null)
        {
            return false; 
        }

        salle.Name = updateModel.Name;
        salle.Capacity = updateModel.Capacity;
        salle.IsActive = updateModel.IsActive;

        try
        {
            _context.Salles.Update(salle);
            await _context.SaveChangesAsync();
            return true; 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating Salle: {ex.Message}");
            return false;
        }
    }
}