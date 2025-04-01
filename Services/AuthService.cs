
namespace api.Services;
using Dtos.Auth;
using Models;
using Configs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService(UsersService userService, IOptions<JwtConfig> jwtConfig)
{
    public async Task<User?> Authenticate(AuthDto authDto)
    {
        var user = await userService.FindByUsername(authDto.Username);

        if (user == null)
        {
          return null;
        }
        else if(!BCrypt.Net.BCrypt.Verify(authDto.Password, user?.Password))
        {
            return null;
        }
        else
        {
            return user;
        }
    }

    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtConfig.Value.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                // Other Claims (roles, permissions, etc.)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(jwtConfig.Value.ExpirationMinutes),
            Issuer = jwtConfig.Value.Issuer,
            Audience = jwtConfig.Value.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}