using Microsoft.EntityFrameworkCore;
using parc.Models;

namespace parc.Services;

public class DeviceService
{
    private readonly AppDbContext _context;

    public DeviceService(AppDbContext context)
    {
        _context = context;
    }
    
    public Device Add(Device device)
    {
        _context.Devices.Add(device);
        _context.SaveChanges();
        return device;
    }
    
    public List<Device> GetAll()
    {
        return _context.Devices.AsNoTracking().ToList();
    }
}