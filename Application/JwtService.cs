using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Infra.Authentication;
using TaskManager.Models.Entities;

namespace TaskManager.Application;

public class JwtService : IJwtService
{
    private JwtSettings _jwtSettings;
    public JwtService(IOptions<JwtSettings> jwtSettingsOptions)
    {
        if (jwtSettingsOptions.Value == null)
        {
            throw new NullReferenceException("");
        }
        _jwtSettings = jwtSettingsOptions.Value;
    }
    private SigningCredentials GetSigningCredentials()
    {
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    }
    public string GenerateToken(User user)
    {
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        SigningCredentials creds = GetSigningCredentials();
        List<Claim> claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, value: user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, value: user.Email),
            new Claim(JwtRegisteredClaimNames.Nickname, value: user.Username)
        };
        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience:  _jwtSettings.Audience,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresAtMinutes),
            signingCredentials: creds,
            claims: claims
        );
        return jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
    }
}
