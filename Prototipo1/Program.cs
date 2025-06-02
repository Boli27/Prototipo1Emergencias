using Microsoft.EntityFrameworkCore;
using Prototipo1.Context;
using Prototipo1.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Cadena de conexion extraido de appsettings.json

var connectionString = builder.Configuration.GetConnectionString("Conexion");
// Servicio para la conexion
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(connectionString)
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Prototipo1 API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa el token JWT con el prefijo 'Bearer'.\nEjemplo: Bearer eyJhbGciOiJIUzI1NiIs..."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

// Servicios para los endpoinds

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ReporteService>();
builder.Services.AddScoped<BrigadistaService>();
builder.Services.AddScoped<UbicacionService>();

// Servicios de autenticacion para jwt

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "Prototipo1",
            ValidAudience = "Prototipo1",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

//Servicios para proteger endpoinds sin usuario logeado y los asignados a brigadista

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EsBrigadista", policy =>
        policy.RequireClaim("esBrigadista", "true"));
});

// *START CORS Configuration*
// Define a CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin() // Allows any origin
                  .AllowAnyMethod()  // Allows any HTTP method (GET, POST, PUT, DELETE, etc.)
                  .AllowAnyHeader(); // Allows any HTTP headers
        });
});
// *END CORS Configuration*

builder.Services.AddControllers(); // This was duplicated, removed one instance

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// *START Use CORS Middleware*
// IMPORTANT: Place UseCors before UseAuthentication and UseAuthorization
app.UseCors("AllowAllOrigins");
// *END Use CORS Middleware*

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();