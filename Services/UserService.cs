using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using parc.models;
using parc.Models;
using parc.Models.shared;

namespace parc.Services;

public class UserService
{
    private readonly AppDbContext _context;
    private readonly AppSettings _appSettings;

    public UserService(AppDbContext context, IOptions<AppSettings> appSettings)
    {
        _context = context;
        _appSettings = appSettings.Value;
    }
    
    public CustomUser Add(CustomUser user)
    {
        user.Role = UserRole.ParcAdmin;
        _context.CustomUsers.Add(user);
        _context.SaveChanges();
        return user;
    }
    
    public CustomUser GetById(int id)
    {
        return _context.CustomUsers.AsNoTracking().SingleOrDefault(x => x.Id == id);
    }
    
    public async Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model)
    {
        var user = await _context.CustomUsers.SingleOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

        if (user == null) return null;

        var token = await generateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }
    
    private async Task<string> generateJwtToken(CustomUser user)
    {
        //Generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = await Task.Run(() =>
        {

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()), new Claim("role", user.Role.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.CreateToken(tokenDescriptor);
        });

        return tokenHandler.WriteToken(token);
    }
}