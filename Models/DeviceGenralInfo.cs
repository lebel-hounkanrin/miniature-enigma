namespace parc.Models;

public class DeviceGenralInfo
{
    public int Id { get; set; } // ID unique
    public string Name { get; set; } // Nom de l'appareil
    public string DeviceType { get; set; } // Type de l'appareil (PC, Serveur, etc.)
    public string Brand { get; set; } // Marque de l'appareil
    public string? Model { get; set; } // Modèle spécifique
    public string? SerialNumber { get; set; } // Numéro de série
    public DateTime? PurchaseDate { get; set; } // Date d'achat
    public DateTime? WarrantyEndDate { get; set; } // Date de fin de garantie
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}