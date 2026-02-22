using Application.CQRS.Command;
using Application.Mapper;
using Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using System.Data;
using WebApiRedarBor.MiddelWare;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api RedarBor",
        Version = "v1"
    });
    options.EnableAnnotations();
});

builder.Services.AddAutoMapper(typeof(AutoMapping));
builder.Services.AddMediatR(cfg =>cfg.RegisterServicesFromAssembly(typeof(CreateEmployeeHandler).Assembly));
builder.Services.AddControllers();

builder.Services.AddDbContext<ContextRedarbor>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CrudConnection")));//EF
builder.Services.AddScoped<IDbConnection>(sp =>new SqlConnection(builder.Configuration.GetConnectionString("CrudConnection")));//Dapepr

builder.Services.AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api RedarBor v1"));
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();