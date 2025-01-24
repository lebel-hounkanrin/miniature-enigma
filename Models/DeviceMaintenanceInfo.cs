namespace parc.Models;

public class DeviceMaintenanceInfo
{
    public int DeviceId { get; set; }
    public DateTime? LastMaintenanceDate { get; set; } // Date de dernière maintenance
    public string MaintenanceHistory { get; set; } // Historique des réparations et maintenances effectuées
}