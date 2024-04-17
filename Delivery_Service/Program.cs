using DeliveryService.Application.Interface;
using DeliveryService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Delivery Service API (Group4-C)", Version = "v1.1" });
    var filePath = Path.Combine(System.AppContext.BaseDirectory, "SwaggerDocumentationFile.xml");
    c.IncludeXmlComments(filePath);
});

builder.Services.AddDbContext<MasterContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("pglConnectionString") ??
throw new InvalidOperationException("Connections string: pglConnectionString was not found"))); 

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();
//Seed Date
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
