using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YPlanning.Data;

var builder = WebApplication.CreateBuilder(args);

// Get environment variables
var CERT_PATH = Environment.GetEnvironmentVariable("CERT_PATH");
var CERT_PASSWORD = Environment.GetEnvironmentVariable("CERT_PASSWORD");
var POSTGRES_PASSWORD = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
var POSTGRESS_IP_ADDRESS = Environment.GetEnvironmentVariable("POSTGRESS_IP_ADDRESS");
var httpsPort = 443;

// Check certificate
if (string.IsNullOrEmpty(CERT_PATH) || string.IsNullOrEmpty(CERT_PASSWORD))
{
    Console.WriteLine("Certificate path or password is missing");
}
else
{
    builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        serverOptions.ListenAnyIP(httpsPort, listenOptions =>
        {
            listenOptions.UseHttps(CERT_PATH, CERT_PASSWORD);
        });
    });
}

// Check PostgreSQL
if (string.IsNullOrEmpty(POSTGRES_PASSWORD) || string.IsNullOrEmpty(POSTGRESS_IP_ADDRESS))
{
    throw new InvalidOperationException("PostgreSQL password or IP address is not configured correctly.");
}

// Build connection string
var connectionString = $"Username=postgres;Password={POSTGRES_PASSWORD};Host={POSTGRESS_IP_ADDRESS};Port=5432;Database=yplanning";

// Connect to database
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(connectionString)
);
 
// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Test database connection
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    try
    {
        dbContext.Database.OpenConnection();
        dbContext.Database.CloseConnection();
        Console.WriteLine("Database connection is successful");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }
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
