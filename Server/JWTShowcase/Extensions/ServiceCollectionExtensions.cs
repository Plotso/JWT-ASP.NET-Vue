namespace JWTShowcase.Extensions;

using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Models;
using Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebService<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        bool swaggerEnabled)
        where TDbContext : DbContext
    {
        services
            .AddDatabase<TDbContext>(configuration)
            .AddApplicationSettings(configuration)
            .AddTokenAuthentication(configuration)
            .AddHealth(configuration)
            .AddAutoMapperProfile(Assembly.GetCallingAssembly())
            .AddControllers();

        if (swaggerEnabled)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen();
        }

        return services;
    }

    public static IServiceCollection AddAutoMapperProfile(
        this IServiceCollection services,
        Assembly assembly)
        => services
            .AddAutoMapper(
                (_, config) => config
                    .AddProfile(new MappingProfile(assembly)),
                Array.Empty<Assembly>());

    public static IServiceCollection AddDatabase<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration)
        where TDbContext : DbContext
        => services
            .AddScoped<DbContext, TDbContext>()
            .AddDbContext<TDbContext>(options => options
                .UseSqlServer(
                    configuration.GetDefaultConnectionString(),
                    sqlOptions => sqlOptions
                        .EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null)));

    public static IServiceCollection AddHealth(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddSqlServer(configuration.GetDefaultConnectionString());

        return services;
    }
    

    public static IServiceCollection AddApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        => services.Configure<ApplicationSettings>(
                configuration.GetSection(nameof(ApplicationSettings)), 
                config => config.BindNonPublicProperties = true);

    public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration configuration, JwtBearerEvents events = null)
    {
        var secret = configuration
            .GetSection(nameof(ApplicationSettings))
            .GetValue<string>(nameof(ApplicationSettings.Secret));

        var key = Encoding.ASCII.GetBytes(secret);

        services
            .AddAuthentication(authentication =>
            {
                authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearer =>
            {
                bearer.RequireHttpsMetadata = false;
                bearer.SaveToken = true;
                bearer.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                if (events != null)
                    bearer.Events = events;
            });

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
    
    private static string GetDefaultConnectionString(this IConfiguration configuration)
        => configuration.GetConnectionString("DefaultConnection");
    
}