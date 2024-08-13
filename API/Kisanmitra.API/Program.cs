using DataAccessLayer.DAL;
using Kisanmitra.API.Repository.Implementations;
using Kisanmitra.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.AspNetCore;
using Kisanmitra.API.Repository.Implementations;
using Microsoft.EntityFrameworkCore.Metadata;
using Kisanmitra.API.Repository.Interface;
using Mapster;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Register DbContexts
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register your repositories
builder.Services.AddScoped<IConsultantCertification, ConsultantCertificationRepo>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
TypeAdapterConfig.GlobalSettings.Scan(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
