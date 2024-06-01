using Microsoft.EntityFrameworkCore;
using YPlanning.Data;

var builder = WebApplication.CreateBuilder(args);

// Get environment variables
var CERT_PATH = Environment.GetEnvironmentVariable("CERT_PATH");
var CERT_PASSWORD = Environment.GetEnvironmentVariable("CERT_PASSWORD");
var PG_PASSWORD = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
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
if (string.IsNullOrEmpty(PG_PASSWORD))
{
    throw new InvalidOperationException("PostgreSQL password is not configured correctly.");
}

// Build connection string
var connectionString = $"Host=localhost;Port=5432;Username=postgres;Password={PG_PASSWORD};Database=yplanning";

// Register the DbContext with the connection string
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(connectionString));

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
