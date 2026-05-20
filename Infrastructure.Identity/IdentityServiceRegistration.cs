using Api.Domain.Entities; // Ensure your ITokenService namespace is included
using DataAccess.EFCore;
using Infrastructure.Identity.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity;

public static class IdentityServiceRegistration
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        // 1. Identity Setup
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

        // 2. JWT Configuration
        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; // 👈 1. ADD THIS TO FORCE JWT OVER COOKIES
        })
        .AddJwtBearer(options => {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!)),
                ValidateIssuer = true,
                ValidIssuer = config["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = config["Jwt:Audience"],
                ValidateLifetime = true,
                ClockSkew = System.TimeSpan.Zero // 👈 2. REMOVES DEFAULT 5-MINUTE SERVER SYNC DELAY
            };

            // 🕵️‍♂️ 3. ADD THIS EVENT HANDLER TO TRACK DETAILED ERROR LOGS
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    System.Diagnostics.Debug.WriteLine($"JWT Validation Failed: {context.Exception.Message}");
                    return Task.CompletedTask;
                }
            };
        });

        // 4. Register the Token Service
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
