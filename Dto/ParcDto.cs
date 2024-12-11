using System.ComponentModel.DataAnnotations;

namespace parc.Dto;

public class ParcDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Location { get; set; }
}