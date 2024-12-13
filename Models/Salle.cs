using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
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
    
    [JsonIgnore]
    public Parc Parc { get; set; }
    
    [SwaggerSchema(ReadOnly = true)] public ICollection<Device> Devices { get; set; } = new List<Device>();
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } =DateTime.UtcNow;
}