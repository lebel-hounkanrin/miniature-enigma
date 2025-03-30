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
    public Salle Add(Salle salle, CustomUser user)
    {
        var parc = _context.Parcs.Where(p => p.OwnerId == user.Id && p.Id == salle.ParcId).FirstOrDefault();
        if (parc == null) throw new UnauthorizedAccessException("You are not authorized to add salles to this parc.");
        _context.Salles.Add(salle);
        _context.SaveChanges();
        return salle;
    }
    
    public List<Salle> GetAll(CustomUser user)
    {
        return _context.Salles
            .Where(s => _context.Parcs.Any(p => p.OwnerId == user.Id && p.Id == s.ParcId))
            .ToList();
        // var parc = _context.Parcs.Where(p => p.OwnerId == user.Id);
        // var salles = _context.Salles.Where(
        //         s => parc.Any(p => p.Id == s.Id)).AsNoTracking().ToList();
        // return salles;
    }
    
    public List<Salle> GetAllForParc(CustomUser user, int parcId)
    {
        var parc = _context.Parcs.FirstOrDefault(p => p.OwnerId == user.Id && p.Id == parcId);
        if (parc == null)
        {
            return new List<Salle>();  
        }

        var salles = _context.Salles
            .Where(s => s.ParcId == parcId)
            .AsNoTracking()
            .ToList();

        return salles;
    }
    
    public Salle GetById(CustomUser user, int id)
    {
        // Récupérer la salle par ID
        var salle = _context.Salles
            .Include(s => s.Devices)  
            .FirstOrDefault(s => s.Id == id);

        if (salle == null)
        {
            return null; 
        }

        var parc = _context.Parcs.FirstOrDefault(p => p.OwnerId == user.Id && p.Id == salle.ParcId);

        if (parc == null)
        {
            return null;
        }

        return salle;
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
    
    public async Task<bool> DeleteSalleAsync(int id, CustomUser user)
    {
        try
        {
            var salle = await _context.Salles.FindAsync(id);
            if (salle == null) return false;

            if (user.Role == UserRole.SuperAdmin)
            {
                _context.Salles.Remove(salle);
                await _context.SaveChangesAsync();
                return true;
            }

            // Vérifier si l'utilisateur est propriétaire du parc contenant la salle
            bool isOwner = await _context.Parcs
                .Where(p => p.OwnerId == user.Id)
                .SelectMany(p => p.Salles)
                .AnyAsync(s => s.Id == id);

            if (!isOwner) return false;

            _context.Salles.Remove(salle);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erreur lors de la suppression de la salle {id}: {e.Message}");
            return false;
        }
    }

}