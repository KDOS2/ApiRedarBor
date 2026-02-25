using Application.CQRS.Command;
using Application.Mapper;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using WebApiRedarBor.MiddelWare;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(AutoMapping));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateEmployeeHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteEmployeeHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LoginHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SoftDeleteEmployeeHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateEmployeeHandler).Assembly));

builder.Services.AddDbContext<ContextRedarbor>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("CrudConnection")));
builder.Services.AddScoped<IDbConnection>(sp =>new SqlConnection(builder.Configuration.GetConnectionString("CrudConnection")));

builder.Services.AddHttpClient();
builder.Services.AddInfrastructure();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api RedarBor",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando Bearer. Ejemplo: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

JsonWebKeySet? cachedJwks = null;
DateTime jwksCacheExpiration = DateTime.MinValue;

async Task<JsonWebKeySet> GetJwksAsync()
{
    if (cachedJwks != null && DateTime.UtcNow < jwksCacheExpiration)
        return cachedJwks;

    using var handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };

    using var client = new HttpClient(handler);
    var jwksJson = await client.GetStringAsync("https://localhost:7118/.well-known/openid-configuration/jwks");
    cachedJwks = new JsonWebKeySet(jwksJson);
    jwksCacheExpiration = DateTime.UtcNow.AddMinutes(5);

    return cachedJwks;
}


try
{
    await GetJwksAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Warning precargando JWKS: {ex.Message}");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:7118";
        options.Audience = "redarbor.api";

        options.BackchannelHttpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        options.SaveToken = true;
        options.IncludeErrorDetails = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            RequireSignedTokens = true,

            ValidIssuer = "https://localhost:7118",
            ValidAudience = "redarbor.api",

            ClockSkew = TimeSpan.FromMinutes(5),
            NameClaimType = "name",
            LogValidationExceptions = true,

            IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
            {
                try
                {
                    var jwks = GetJwksAsync().GetAwaiter().GetResult();

                    if (!string.IsNullOrEmpty(kid))
                    {
                        var key = jwks.GetSigningKeys().FirstOrDefault(k => k.KeyId == kid);
                        if (key != null)
                            return new[] { key };
                    }
                    return jwks.GetSigningKeys();
                }
                catch (Exception ex)
                {
                    return Array.Empty<SecurityKey>();
                }
            }
        };


        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context => { return Task.CompletedTask; },
            OnTokenValidated = context => { return Task.CompletedTask; },
            OnMessageReceived = context =>
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if (!string.IsNullOrEmpty(token))
                {
                    try
                    {
                        var handler = new JwtSecurityTokenHandler();
                        var jwt = handler.ReadJwtToken(token);
                    }
                    catch { }
                }
                return Task.CompletedTask;
            },
            OnChallenge = context => { return Task.CompletedTask; }
        };
    });



builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api RedarBor v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();