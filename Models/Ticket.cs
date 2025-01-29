namespace parc.Models;

public class Ticket
{
    public int Id {get; set;}
    private int PosteId {get; set;}
    public string Description {get; set;}
    public int Priority {get; set;}
    public DateTime CreatedDated {get; set;}
    public DateTime UpdatedDated {get; set;}
    public int? UserId {get; set;}
}