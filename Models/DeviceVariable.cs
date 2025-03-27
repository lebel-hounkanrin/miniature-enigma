using System.Text.Json.Serialization;

namespace parc.Models;

public class DeviceVariable
{
    public int Id { get; set; }
    public int DeviceId { get; set; }
    public string? FreeStorage { get; set; }
    public string? FreeRamSize { get; set; }
    
    public string? DiskRead { get; set; }
    public string? DiskWrite { get; set; }
    
    public string? NetSend { get; set; }
    public string? NetReceive { get; set; }
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [JsonIgnore]
    public DeviceGenralInfo? Device { get; set; }
}