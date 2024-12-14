using System.ComponentModel.DataAnnotations;

namespace parc.Shared;


public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        string email = value as string;
        
        if (string.IsNullOrEmpty(email))
        {
            return ValidationResult.Success;
        }
        var _context = (AppDbContext)validationContext.GetService(typeof(AppDbContext));
        bool emailExists = _context.CustomUsers.Any(u => u.Email == email);
        if(!emailExists) { return ValidationResult.Success; }
        return new ValidationResult(GetErrorMessage(value.ToString()));
    }
    
    public string GetErrorMessage(string email)
    {
        return $"Email {email} is already in use.";
    }
}