namespace parc.Models;

public class DeviceMaintenanceInfo
{
    public int DeviceId { get; set; }
    public ICollection<Ticket> Tickets { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public string? Observation { get; set; }
    public int? AuthorId { get; set; }
}