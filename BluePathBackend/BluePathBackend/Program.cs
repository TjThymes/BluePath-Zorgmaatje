using BluePath_Backend.Data;
using BluePathBackend.Objects;
using BluePathBackend.Repos;
using BluePathBackend.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using BluePath_Backend.Interfaces;
using System.Security.Claims;
using BluePathBackend.Other;

var builder = WebApplication.CreateBuilder(args);

// ✅ Repositories
builder.Services.AddScoped<IDiaryRepository, DiaryRepository>();
builder.Services.AddScoped<IPatientInfoRepository, PatientInfoRepository>();
builder.Services.AddScoped<IUserRouteProgressRepository, UserRouteProgressRepository>();
builder.Services.AddScoped<IRouteRepository, RouteRepository>();
builder.Services.AddScoped<IUserAvatarRepository, UserAvatarRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<JwtTokenGenerator>();
// ✅ Configuratie
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// ✅ Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString")));

// ✅ Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
builder.Logging.AddConsole();


var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "BluePathAPI",
            ValidAudience = "BluePathAPI",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!))
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("❌ Authentication failed:");
                Console.WriteLine(context.Exception.ToString());
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("✅ Token validated for user: " + context.Principal.Identity?.Name);
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();


// ✅ Swagger met veilige JWT-support
builder.Services.AddSwaggerGen(c =>
{
    // 👇 This line is missing in your current code
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BluePath API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
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
            Array.Empty<string>()
        }
    });
});


// ✅ Build + Middleware
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v/swagger.json", "BluePath API v2");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
