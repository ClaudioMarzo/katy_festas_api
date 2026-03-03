using System.Text;
using FluentValidation;
using Scalar.AspNetCore;
using KatyFestas.API.Middlewares;
using FluentValidation.AspNetCore;
using KatyFestas.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using KatyFestas.Application.Services;
using Microsoft.AspNetCore.RateLimiting;
using KatyFestas.Infrastructure.Security;
using KatyFestas.Infrastructure.Persistence;
using KatyFestas.Domain.Interfaces.Services;
using KatyFestas.Application.Validators.Item;
using KatyFestas.Application.Interfaces.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// ═══════════════════ BANCO DE DADOS ═══════════════════
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") 
    ?? builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string não encontrada");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// ═══════════════════ UNIT OF WORK & REPOSITORIES ═══════════════════
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// ═══════════════════ APPLICATION SERVICES ═══════════════════
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// ═══════════════════ JWT AUTHENTICATION ═══════════════════
var jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY")
    ?? builder.Configuration["Jwt:SecretKey"]
    ?? throw new InvalidOperationException("JWT SecretKey não encontrada");

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
var encryptionKey = Environment.GetEnvironmentVariable("ENCRYPTION_KEY")
    ?? builder.Configuration["Encryption:Key"]
    ?? throw new InvalidOperationException("Encryption Key não encontrada");

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
    .AddNpgSql(connectionString);

// ═══════════════════ CONTROLLERS + SCALAR API DOCS ═══════════════════
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, _) =>
    {
        // Pega o scheme real (HTTPS atrás de proxy como Railway)
        var request = context.ApplicationServices
            .GetRequiredService<IHttpContextAccessor>()
            .HttpContext?.Request;

        if (request is not null)
        {
            document.Servers =
            [
                new OpenApiServer 
                { 
                    Url = $"{request.Scheme}://{request.Host}" 
                }
            ];
        }

        return Task.CompletedTask;
    });
});

builder.Services.AddHttpContextAccessor();

// ═══════════════════ CORS ═══════════════════
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// ═══════════════════ AUTO MIGRATE ═══════════════════
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// ═══════════════════ FORWARDED HEADERS (Railway/Proxy) ═══════════════════
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

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
