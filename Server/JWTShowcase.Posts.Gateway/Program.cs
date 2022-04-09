using System.Reflection;
using JWTShowcase.Extensions;
using JWTShowcase.Middlewares;
using JWTShowcase.Posts.Gateway.Services;
using JWTShowcase.Posts.Gateway.Services.Posts;
using JWTShowcase.Services;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddAutoMapperProfile(Assembly.GetExecutingAssembly())
    .AddTokenAuthentication(builder.Configuration)
    .AddScoped<ICurrentTokenService, CurrentTokenService>()
    .AddTransient<JwtAuthenticationMiddleware>()
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    
    .AddControllers();

var serviceEndpoints = builder.Configuration
    .GetSection(nameof(ServiceEndpoints))
    .Get<ServiceEndpoints>(config => config.BindNonPublicProperties = true);

builder.Services.AddRefitClient<IPublicPostsService>().WithConfiguration(serviceEndpoints.Posts);
builder.Services.AddRefitClient<IPostsService>().WithConfiguration(serviceEndpoints.Posts);
builder.Services.AddRefitClient<ICommentsService>().WithConfiguration(serviceEndpoints.Posts);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseJwtHeaderAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();