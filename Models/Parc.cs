namespace parc.Models;

public class Parc
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public DateTime CratedAt { get; set; }
    public int OwnerId { get; set; }
}