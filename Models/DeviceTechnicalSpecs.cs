using System.Text.Json.Serialization;

namespace parc.Models;

public class DeviceTechnicalSpecs
{
    public int Id { get; set; }
    public int DeviceId { get; set; }
    [JsonIgnore]
    public DeviceGenralInfo? Device { get; set; }
    public string? OperatingSystem { get; set; } // Système d'exploitation
    public string? Processor { get; set; } // Processeur (modèle et spécifications)
    public int? TotalRamSize { get; set; } // Taille de la RAM (en Go)
    public string? TotalStorage { get; set; } // Type et taille du stockage (SSD, HDD, etc.)
    public string? GraphicsCard { get; set; } // Carte graphique
    
    public int? FreeRamSize { get; set; }
    public int? FreeStorage { get; set; }
    
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}