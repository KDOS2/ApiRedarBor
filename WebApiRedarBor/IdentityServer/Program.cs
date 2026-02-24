
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;
using IdentityServer;
using IdentityServer.Service.IdentityServer.Services;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContextRedarbor>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CrudConnection")));

// Configura IdentityServer
builder.Services
    .AddIdentityServer()
    .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes)
    .AddInMemoryApiResources(IdentityServerConfig.ApiResources)
    .AddInMemoryClients(IdentityServerConfig.Clients)
    .AddDeveloperSigningCredential()
    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

builder.Services.AddTransient<IProfileService, ProfileService>();
builder.Services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();

var app = builder.Build();
app.UseIdentityServer();

app.MapGet("/", () => "Servidor de identidad prueba");

app.Run();