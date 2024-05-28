var builder = WebApplication.CreateBuilder(args);

// Read certificate settings from environment variables
var certPath = Environment.GetEnvironmentVariable("CERT_PATH");
var certPasswordFilePath = "/app/cert_password.txt";

if (string.IsNullOrEmpty(certPath) || !File.Exists(certPasswordFilePath))
{
    throw new InvalidOperationException("Certificate path or password is not configured correctly.");
}

var certPassword = File.ReadAllText(certPasswordFilePath).Trim();

// Configure Kestrel (Dockers uses it) to use HTTPS with the provided certificate
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(443, listenOptions =>
    {
        listenOptions.UseHttps(certPath, certPassword);
    });
});

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
