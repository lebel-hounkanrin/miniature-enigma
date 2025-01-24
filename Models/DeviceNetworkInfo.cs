namespace parc.Models;

public class DeviceNetworkInfo
{
    public int DeviceId { get; set; }
    public string IpAddress { get; set; } // Adresse IP
    public string MacAddress { get; set; } // Adresse MAC
    public string Hostname { get; set; } // Nom d'hôte
    public string Network { get; set; } // Réseau auquel l'appareil est connecté
    public string ConnectionType { get; set; } // Type de connexion (Wi-Fi, Ethernet, etc.)
}