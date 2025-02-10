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

    public List<DeviceGenralInfo> GetAll()
    {
        return _context.DeviceGenralInfos.ToList();
    }

    public DeviceGenralInfo GetById(int id)
    {
        return _context.DeviceGenralInfos.Find(id);
    }

    public DeviceGenralInfo Update(int id, DeviceGenralInfo data)
    {
        var deviceGenralInfo = _context.DeviceGenralInfos.Find(id);
        if (deviceGenralInfo != null)
        {
            deviceGenralInfo.Name = data.Name ?? deviceGenralInfo.Name;
            deviceGenralInfo.DeviceType = data.DeviceType ?? deviceGenralInfo.DeviceType;
            deviceGenralInfo.Brand = data.Brand ?? deviceGenralInfo.Brand;
            deviceGenralInfo.Model = data.Model ?? deviceGenralInfo.Model;
            deviceGenralInfo.SerialNumber = data.SerialNumber ?? deviceGenralInfo.SerialNumber;
            deviceGenralInfo.PurchaseDate = data.PurchaseDate ?? deviceGenralInfo.PurchaseDate;
            deviceGenralInfo.WarrantyEndDate = data.WarrantyEndDate ?? deviceGenralInfo.WarrantyEndDate;
            deviceGenralInfo.UpdatedAt = DateTime.UtcNow;;
            
            var updatedData = _context.DeviceGenralInfos.Update(deviceGenralInfo);
            _context.SaveChanges();
            return updatedData.Entity;
        }
        
        return null;
    }
}