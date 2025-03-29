using System.ComponentModel.DataAnnotations;

namespace parc.Models;

public class DeviceType
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(100)]
    public string? Description { get; set; }
}