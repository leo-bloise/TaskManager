using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Application;

namespace TaskManager.Infra.Authentication;

public static class AuthenticationSetup
{
    public static void ConfigureJwt(this WebApplicationBuilder builder)
    {
        JwtSettings jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
        if (jwtSettings == null)
        {
            throw new NullReferenceException("JwtSettings is null");
        }
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
        builder.Services.AddScoped<IJwtService, JwtService>();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Audience = jwtSettings.Audience;
                options.TokenValidationParameters = new TokenValidationParameters()
                {                    
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });
    }
}