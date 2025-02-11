using parc.Models;

namespace parc.Services;

public class DeviceNetworkInfoService
{
    private readonly AppDbContext _context;

    public DeviceNetworkInfoService(AppDbContext context)
    {
        _context = context;
    }
    
    public DeviceNetworkInfo Add(DeviceNetworkInfo deviceNetworkInfo)
    {
        try
        {
            var device = _context.DeviceNetworkInfo.Add(deviceNetworkInfo);
            _context.SaveChanges();
            return device.Entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public DeviceNetworkInfo GetById(int id)
    {
        return _context.DeviceNetworkInfo.Find(id);
    }
    
    public DeviceNetworkInfo Update(int id, DeviceNetworkInfo data)
    {
        try
        {
            var entity = _context.DeviceNetworkInfo.Find(id);
            if (entity == null)
                return null;
            entity.IpAddress = data.IpAddress ?? entity.IpAddress;
            entity.MacAddress = data.MacAddress ?? entity.MacAddress;
            entity.Hostname = data.Hostname ?? entity.Hostname;
            entity.Network = data.Network ?? entity.Network;
            entity.ConnectionType = data.ConnectionType ?? entity.ConnectionType;
            
            entity.UpdatedAt = DateTime.UtcNow;
            // var updatedData = _context.DeviceNetworkInfo.Update(data);
            _context.SaveChanges();
            return entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}