namespace JWTShowcase.Extensions;

using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Middlewares;
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
}
