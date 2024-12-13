using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Extensions;
using parc.Models.shared;

namespace parc.Models;

public class Device
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Type { get; set; }

    [Required] [MaxLength(100)] public string Status { get; set; } = DeviceStatus.Active.GetDisplayName();
    
    public string? IpAdress { get; set; }
    
    [Required] public string Description { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    [Required]
    public int SalleId { get; set; }
    
    [JsonIgnore]
    public Salle? Salle { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAp { get; set; } = DateTime.UtcNow;
}