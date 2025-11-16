using BusinessLayer.Services.Implementations;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Middleware;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookHubDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Add services to the container.

builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IPurchaseItemService, PurchaseItemService>();
builder.Services.AddScoped<IWishlistItemService, WishlistItemService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IImageService, ImageService>();

// register upload service as the BusinessLayer FileSystemUploadService using a factory
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
    var connectionString = builder.Configuration.GetConnectionString("MongoDB")
                           ?? "mongodb://localhost:27017";
    return new MongoClient(connectionString);
});

builder.Services.AddSingleton<ILogService, MongoLogService>();


var app = builder.Build();

app.ConfigureStaticFileServing(app.Configuration, app.Environment);

builder.Services.AddLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<RequestTimingMiddleware>();
app.UseMiddleware<TokenAuthenticationMiddleware>();
app.UseMiddleware<AuditLogMiddleware>();

// Configure the HTTP request pipeline.

app.UseAuthorization();
app.UseMiddleware<JsonXmlMiddleware>();
app.MapControllers();

Console.WriteLine($"Swagger UI available at: http://localhost:5000/swagger/index.html");

app.Run();
