using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Data;


var builder = WebApplication.CreateBuilder(args);

// ================================
// SERVICES
// ================================
builder.Services.AddControllers();

// ================================
// CONNECTION STRING
// ================================
var conn = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(conn))
{
    throw new Exception("Database connection string not found.");
}

Console.WriteLine("DB connection loaded");

// ================================
// DATABASE (MSSQL  FIXED)
// ================================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(conn));   //  CHANGED HERE

// ================================
// CORS
// ================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins(
            "http://localhost:4200",
            "https://expencebook.netlify.app"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// ================================
// SWAGGER
// ================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ================================
// MIDDLEWARE
// ================================
app.UseRouting();

app.UseCors("AllowAngular");

// Swagger (enabled always)
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

// ================================
// TEST ROUTE
// ================================
app.MapGet("/", () => "ExpenseTracker API is running!");

// ================================
// CONTROLLERS
// ================================
app.MapControllers();

app.Run();