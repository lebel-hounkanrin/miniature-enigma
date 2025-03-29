namespace parc.Models;

public class Ticket
{
    public int Id {get; set;}
    public string Title {get; set;}
    public string Description {get; set;}
    public int Priority {get; set;} // should be a range(1, 3) 1: low, 2: medium, 3: hight
    public DateTime CreatedDated {get; set;}
    public DateTime UpdatedDated {get; set;}
    public int DeviceId { get; set; } = 0;
    public int? UserId {get; set;} // created by
}