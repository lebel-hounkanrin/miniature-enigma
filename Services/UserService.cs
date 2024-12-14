using parc.Models;
using parc.Models.shared;

namespace parc.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }
    
    public CustomUser Add(CustomUser user)
    {
        user.Role = UserRole.SuperAdmin;
        _context.CustomUsers.Add(user);
        _context.SaveChanges();
        return user;
    }
}