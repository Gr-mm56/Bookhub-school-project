using BusinessLayer.Services.Implementations;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Middleware;
using Middleware.Services;

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

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("MongoDB")
                           ?? "mongodb://localhost:27017";
    return new MongoClient(connectionString);
});

builder.Services.AddSingleton<ILogService, MongoLogService>();


var app = builder.Build();

builder.Services.AddLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<RequestTimingMiddleware>();
app.UseMiddleware<TokenAuthenticationMiddleware>();

// Configure the HTTP request pipeline.

app.UseAuthorization();
app.UseMiddleware<JsonXmlMiddleware>();
app.MapControllers();

Console.WriteLine($"Swagger UI available at: http://localhost:5000/swagger/index.html");

app.Run();
