using System.ComponentModel.DataAnnotations;

namespace parc.Models;

public class Parc
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Location { get; set; }
    
    public DateTime CratedAt { get; set; }
    
    public int OwnerId { get; set; }
}