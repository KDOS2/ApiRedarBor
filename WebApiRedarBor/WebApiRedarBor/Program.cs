using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Crud Api RedarBor",
        Version = "v1"
    });
});

//builder.Services.AddAutoMapper(typeof(AutoMapping));

builder.Services.AddControllers();

builder.Services.AddDbContext<ContextRedarbor>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CrudConnection"))
);

//builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api RedarBor v1"));
}

///app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();