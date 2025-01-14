using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using parc.Models;
using parc.Models.shared;

namespace parc.Helpers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute: Attribute, IAuthorizationFilter
{
    private readonly string _requiredRole;
    public AuthorizeAttribute(string requiredRole = null)
    {
        _requiredRole = requiredRole;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = (CustomUser?)context.HttpContext.Items["User"];
        if (user == null)
        {
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            return;
        }
        
        
        if (_requiredRole != null && user.Role.ToString() != _requiredRole)
        {
            if (user.Role.ToString().ToLower() != UserRole.SuperAdmin.ToString().ToLower())
            {
                context.Result = new JsonResult(new { message = "Forbidden" })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
                return;
            }
        }
    }
    
}