using BluePath_Backend.Data;
using BluePathBackend.Objects;
using BluePath_Backend.Repositories;
using BluePathBackend.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BluePath_Backend.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IDiaryRepository, DiaryRepository>();
builder.Services.AddScoped<IPatientInfoRepository, PatientInfoRepository>();
builder.Services.AddScoped<IUserRouteProgressRepository, UserRouteProgressRepository>();
builder.Services.AddScoped<IRouteRepository, RouteRepository>();
builder.Services.AddScoped<IUserAvatarRepository, UserAvatarRepository>();



// ✅ Configuration van appsettings.json en omgeving-specifiek bestand
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// ✅ SQL Server DB Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString")));

// ✅ Identity + UserRepo
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// ✅ JWT Configuratie
var jwtKeyString = builder.Configuration["Jwt:Key"];
if (string.IsNullOrWhiteSpace(jwtKeyString))
{
    throw new InvalidOperationException("JWT key is missing from configuration.");
}
var jwtKey = Encoding.UTF8.GetBytes(jwtKeyString);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
            ValidateIssuerSigningKey = true
        };
    });

// ✅ Swagger, Controllers, Authorization
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
