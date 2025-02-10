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
}