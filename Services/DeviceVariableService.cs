using parc.Models;

namespace parc.Services;

public class DeviceVariableService
{
    private readonly AppDbContext _context;
    public DeviceVariableService(AppDbContext context)
    {
        _context = context;
    }   
    
    public DeviceVariable Add(DeviceVariable data)
    {
        try
        {
            var device = _context.DeviceVariables.Add(data);
            _context.SaveChanges();
            return device.Entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public List<DeviceVariable> GetAll(int deviceId)
    {
        try
        {
            return _context.DeviceVariables.Where(x => x.DeviceId == deviceId).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
    
    public DeviceVariable GetById(int id)
    {
        return _context.DeviceVariables.Find(id);
    }
}