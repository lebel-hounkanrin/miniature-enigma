using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using parc.Models.shared;
using parc.Shared;

namespace parc.Models;

public class CustomUser
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public string LastName { get; set; }
    public required string Username { get; set; }
    
    [EmailAddress]
    [UniqueEmail(ErrorMessage = "This email address is already registered.")]
    public string Email { get; set; }
    
    public UserRole Role { get; set; }

    [JsonIgnore]
    public string Password { get; set; }
    public bool isActive { get; set; }
}