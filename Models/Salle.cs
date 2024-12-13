using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;


namespace parc.Models;

public class Salle
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    public int Capacity { get; set; }
    
    [SwaggerSchema(WriteOnly = true)]
    [Required]
    public int ParcId { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } =DateTime.UtcNow;
}