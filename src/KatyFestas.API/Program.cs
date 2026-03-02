using System.Text;
using FluentValidation;
using KatyFestas.API.Middlewares;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.RateLimiting;
using KatyFestas.Infrastructure.Security;
using KatyFestas.Infrastructure.Persistence;
using KatyFestas.Domain.Interfaces.Services;
using KatyFestas.Application.Validators.Item;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using KatyFestas.Domain.Interfaces;
using KatyFestas.Application.Interfaces.Services;
using KatyFestas.Application.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// ═══════════════════ BANCO DE DADOS ═══════════════════
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ═══════════════════ UNIT OF WORK & REPOSITORIES ═══════════════════
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// ═══════════════════ APPLICATION SERVICES ═══════════════════
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// ═══════════════════ JWT AUTHENTICATION ═══════════════════
var jwtKey = builder.Configuration["Jwt:SecretKey"]!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// ═══════════════════ ENCRYPTION SERVICE ═══════════════════
var encryptionKey = builder.Configuration["Encryption:Key"]!;
builder.Services.AddSingleton<IEncryptionService>(new EncryptionService(encryptionKey));

// ═══════════════════ FLUENTVALIDATION ═══════════════════
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateItemValidator>();

// ═══════════════════ RATE LIMITING ═══════════════════
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", limiterOptions =>
    {
        limiterOptions.PermitLimit = 60;
        limiterOptions.Window = TimeSpan.FromMinutes(1);
    });
});

// ═══════════════════ HEALTH CHECKS ═══════════════════
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!);

// ═══════════════════ CONTROLLERS + SCALAR API DOCS ═══════════════════
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// ═══════════════════ CORS ═══════════════════
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// ═══════════════════ MIDDLEWARES  ═══════════════════
app.UseMiddleware<CorrelationIdMiddleware>();  //  Gera CorrelationId
app.UseMiddleware<ExceptionHandlingMiddleware>(); // Captura exceções

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors("AllowAll");
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
