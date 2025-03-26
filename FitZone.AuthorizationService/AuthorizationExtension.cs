using FitZone.AuthorizationService.Roles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.AuthorizationService
{
    public static class AuthorizationExtensions
    {
        public static void AddCustomAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtKey = configuration["Jwt:Key"];
            // Adaugă autentificarea JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtKey))
                };
            });

            services.AddSingleton<IAuthorizationHandler, ClientRoleHandler>();
            services.AddSingleton<IAuthorizationHandler, EmployeeRoleHandler>();
            services.AddSingleton<IAuthorizationHandler, PersonalTrainerRoleHandler>();
            services.AddSingleton<IAuthorizationHandler, GymManagerRoleHandler>();


            // Configurează politicile de autorizare
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireClientRole", policy =>
                    policy.Requirements.Add(new ClientRoleRequirment()));

                options.AddPolicy("RequireEmployeeRole", policy =>
                    policy.Requirements.Add(new EmployeeRoleRequirment()));

                options.AddPolicy("RequirePersonalTrainerRole", policy =>
                    policy.Requirements.Add(new PersonalTrainerRoleRequirment()));

                options.AddPolicy("RequireGymManagerRole", policy =>
                    policy.Requirements.Add(new GymManagerRoleRequirment()));

                options.AddPolicy("RequireAppManagerRole", policy =>
                    policy.Requirements.Add(new AppManagerRoleRequirment()));
            });


        }
    }
}
