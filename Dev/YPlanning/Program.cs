using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Read certificate settings from environment variables (inside Docker container)
var certPath = Environment.GetEnvironmentVariable("CERT_PATH");
var certPassword = Environment.GetEnvironmentVariable("CERT_PASSWORD");
var httpsPort = 443;

// Configure Kestrel (Dockers uses it) to use HTTPS with the provided certificate
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(httpsPort, listenOptions =>
    {
        listenOptions.UseHttps(certPath, certPassword);
    });
});

// Read PostgreSQL password from environment variable
/*var pgPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

if (string.IsNullOrEmpty(pgPassword))
{
    throw new InvalidOperationException("PostgreSQL password is not configured correctly.");
}

// Build connection string
var connectionString = $"Host=localhost;Port=5432;Username=your_username;Password={pgPassword};Database=your_database";

// Register the DbContext with the connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
*/

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
