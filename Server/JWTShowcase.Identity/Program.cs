using JWTShowcase.Extensions;
using JWTShowcase.Identity.Data;
using JWTShowcase.Identity.Extensions;
using JWTShowcase.Identity.Services;
using JWTShowcase.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddWebService<IdentityDbContext>(builder.Configuration, swaggerEnabled: true)
    .AddUserStorage()
    .AddTransient<IDataSeeder, IdentitySeeder>()
    .AddTransient<IIdentityService, IdentityService>()
    .AddTransient<ITokenGenerator, TokenGenerator>();

var app = builder.Build();
app
    .UseWebService(app.Environment, swaggerEnabled: true)
    .SeedData();

app.Run();
