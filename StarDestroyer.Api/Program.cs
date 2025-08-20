using Microsoft.EntityFrameworkCore;
using StarDestroyer.Domain;
using System.Net;

// DEVELOPMENT ONLY: Bypass SSL validation for external APIs with expired certificates
if (args.Contains("--development") || Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
{
    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Entity Framework
builder.Services.AddDbContext<StarDestroyerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add repositories and services
builder.Services.AddScoped<IStarshipService, StarshipService>();
builder.Services.AddScoped<ISwapiService, SwapiService>();

// Configure HttpClient for SwapiService with SSL bypass for development
builder.Services.AddHttpClient<ISwapiService, SwapiService>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
    client.BaseAddress = new Uri("https://swapi.info/api/");
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();
    if (builder.Environment.IsDevelopment())
    {
        // Skip SSL validation in development
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
    }
    return handler;
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowReactApp");

app.UseRouting();
app.MapControllers();

app.Run();
