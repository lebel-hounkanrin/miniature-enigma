namespace parc.Models;

public class DeviceTechnicalSpecs
{
    public int DeviceId { get; set; }
    public string OperatingSystem { get; set; } // Système d'exploitation
    public string Processor { get; set; } // Processeur (modèle et spécifications)
    public int RamSize { get; set; } // Taille de la RAM (en Go)
    public string Storage { get; set; } // Type et taille du stockage (SSD, HDD, etc.)
    public string GraphicsCard { get; set; } // Carte graphique
}