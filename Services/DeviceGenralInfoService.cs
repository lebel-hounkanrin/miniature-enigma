using parc.Models;

namespace parc.Services;

public class DeviceGenralInfoService
{
    private readonly AppDbContext _context;
    
    public DeviceGenralInfoService(AppDbContext context)
    {
        _context = context;
    }

    public DeviceGenralInfo Add(DeviceGenralInfo deviceGenralInfo)
    {
        try
        {
            var device = _context.DeviceGenralInfos.Add(deviceGenralInfo);
            _context.SaveChanges();
            return device.Entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}