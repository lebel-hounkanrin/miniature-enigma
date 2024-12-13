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
    
    public ICollection<Salle>? Salles { get; set; } = new List<Salle>();
    
    public int OwnerId { get; set; }
    
    // public User Owner { get; set; } 
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}