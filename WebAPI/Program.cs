using BusinessLayer.Services.Implementations;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Middleware;
using MongoDB.Driver;
using Serilog;
using WebAPI.Extensions;

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();
builder.Services.AddDbContext<BookHubDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMemoryCache();

builder.Services.AddBusinessServices();
builder.Services.AddFileSystemUploadService(builder.Configuration, builder.Environment);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookHub", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter the API key as follows: Bearer YourHardcodedToken",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

builder.Services.AddSingleton<IMongoClient>(_ =>
{
    var connectionString = builder.Configuration.GetConnectionString("MongoDB");

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException(
            "MongoDB connection string is not configured. " +
            "Please set 'ConnectionStrings:MongoDB' in appsettings.json or environment variables.");
    }

    return new MongoClient(connectionString);
});

builder.Services.AddSingleton<ILogService, MongoLogService>();

var app = builder.Build();

// Ensure database is created and migrations are applied
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<BookHubDbContext>();
    
    try
    {
        dbContext.Database.Migrate();
        Log.Information("Database migrations applied successfully.");
    }
    catch (Exception ex)
    {
        Log.Error(ex, "An error occurred while applying database migrations.");
    }
}

app.ConfigureStaticFileServing(app.Configuration, app.Environment);

builder.Services.AddLogging();

// Enable Swagger in Development and Docker environments
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Docker")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<TokenAuthenticationMiddleware>();

// Configure the HTTP request pipeline.

app.UseAuthorization();
app.UseMiddleware<JsonXmlMiddleware>();
app.MapControllers();

Console.WriteLine($"Swagger UI available at: http://localhost:5000/swagger/index.html");

app.Run();
