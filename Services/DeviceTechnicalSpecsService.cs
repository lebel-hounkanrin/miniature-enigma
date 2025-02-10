using parc.Models;

namespace parc.Services;

public class DeviceTechnicalSpecsService
{
    private readonly AppDbContext _context;

    public DeviceTechnicalSpecsService(AppDbContext context)
    {
        _context = context;
    }
    
    public DeviceTechnicalSpecs Add(DeviceTechnicalSpecs data)
    {
        try
        {
            var entity = _context.DeviceTechnicalSpecs.Add(data);
            _context.SaveChanges();
            return entity.Entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public DeviceTechnicalSpecs GetById(int id)
    {
        return _context.DeviceTechnicalSpecs.Find(id);
    }

    public DeviceTechnicalSpecs Update(int id, DeviceTechnicalSpecs data)
    {
        try
        {
            var entity = _context.DeviceTechnicalSpecs.Find(id);
            if (entity == null)
                return null;
            entity.OperatingSystem = data.OperatingSystem ?? entity.OperatingSystem;
            entity.Processor = data.Processor ?? entity.Processor;
            entity.TotalRamSize = data.TotalRamSize ?? entity.TotalRamSize;
            entity.TotalStorage = data.TotalStorage ?? entity.TotalStorage;
            entity.GraphicsCard = data.GraphicsCard ?? entity.GraphicsCard;
            
            entity.UpdatedAt = DateTime.UtcNow;
            var updatedData = _context.DeviceTechnicalSpecs.Update(data);
            _context.SaveChanges();
            return updatedData.Entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
}