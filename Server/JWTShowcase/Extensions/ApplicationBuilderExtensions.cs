namespace JWTShowcase.Extensions;

using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Middlewares;
using Services;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseWebService(this IApplicationBuilder app, IWebHostEnvironment env, bool swaggerEnabled)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            if (swaggerEnabled)
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }

        app
            .UseHttpsRedirection()
            .UseRouting()
            .UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod())
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapControllers();
            });

        return app;
    }

    public static IApplicationBuilder UseJwtHeaderAuthentication(this IApplicationBuilder app)
        => app
            .UseMiddleware<JwtAuthenticationMiddleware>()
            .UseAuthentication();

    public static IApplicationBuilder SeedData(
        this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;

        var db = serviceProvider.GetRequiredService<DbContext>();

        db.Database.Migrate();

        var seeders = serviceProvider.GetServices<IDataSeeder>();

        foreach (var seeder in seeders)
            seeder.SeedData();

        return app;
    }
}
