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
    
    public Device GetById(int id)
    {
        return _context.Devices.Where(s => s.Id == id).FirstOrDefault();
    }
    
    public async Task<bool> Update(int id, Device deviceModel)
    {
        var device = await _context.Devices.FirstOrDefaultAsync(s => s.Id == id);
        if (device == null)
        {
            return false; 
        }

        device.Name = deviceModel.Name;
        device.Description = deviceModel.Description;
        device.IsActive = deviceModel.IsActive;
        device.Status = deviceModel.Status;
        device.Type = deviceModel.Type;

        try
        {
            _context.Devices.Update(device);
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