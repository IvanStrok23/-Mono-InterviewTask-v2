using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MonoTask.Application.Services;
using MonoTask.Application.Services.Vehicle;
using MonoTask.Core.Interfaces.Services;
using MonoTask.Infrastructure.Data.DbContexts;
using MonoTask.Infrastructure.Data.Interfaces;
using MonoTask.UI.WebApi.Auth;
using MonoTask.UI.WebApi.Auth.Handlers;

public static class ServiceExtensions
{
    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MonoTask API", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter JWT with Bearer prefix",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
    }

    public static void AddDatabaseContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IDataContext, DataContext>();
    }

    public static void AddAuthenticationServices(this IServiceCollection services)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddScheme<AuthenticationSchemeOptions, CustomAuthenticationHandler>(JwtBearerDefaults.AuthenticationScheme, (o) => { });

        services.AddScoped<CustomAuthenticationFunctionHandler>();
    }

    public static void AddAuthorizationServices(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("IsClient", policy => policy.Requirements.Add(new IsClientRequirement()));
        });

        services.AddSingleton<IAuthorizationHandler, IsClientHandler>();
    }

    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserServices, UserServices>();
        services.AddScoped<IVehicleModelService, VehicleModelService>();
        services.AddScoped<IVehicleBrandService, VehicleBrandService>();
    }

    public static void AddAutoMapperServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}