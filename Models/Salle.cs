namespace parc.Models;

public class Salle
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public int ParcId { get; set; }
    public bool isActive { get; set; }
    public DateTime CratedAt { get; set; }
}