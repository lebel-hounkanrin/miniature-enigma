using System.Text.Json.Serialization;

namespace parc.Models;

public class DeviceVariable
{
    public int Id { get; set; }
    public int DeviceId { get; set; }
    public int? FreeStorage { get; set; }
    public int? FreeRamSize { get; set; }
    
    public int? DiskRead { get; set; }
    public int? DiskWrite { get; set; }
    
    public int? NetSend { get; set; }
    public int? NetReceive { get; set; }
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [JsonIgnore]
    public DeviceGenralInfo? Device { get; set; }
}