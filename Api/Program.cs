using Api.Infrastructure;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);

// 1. In-Memory Cache register kiya (Blacklisted / Logged-out tokens ko temporarily store karne ke liye)
builder.Services.AddMemoryCache();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwt = builder.Configuration.GetSection("Jwt");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwt["Key"]!)),
            ClockSkew = TimeSpan.Zero // Strict expiry check (no default 5-minute leeway)
        };

        // 2. YAHAN TOKEN INVALIDATE CHECK HO RAHA HAI:
        // Har authenticated request par token check hoga ki kya user logout kar chuka hai
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                var cache = context.HttpContext.RequestServices.GetRequiredService<IMemoryCache>();

                // Request header se incoming token nikaala
                var authHeader = context.Request.Headers["Authorization"].ToString();
                var token = authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
                    ? authHeader.Substring("Bearer ".Length).Trim()
                    : authHeader.Trim();

                // Agar yeh token blacklist cache me present hai, toh request ko block (401) kar do
                if (cache.TryGetValue($"blacklist:{token}", out _))
                {
                    context.Fail("This token has been revoked / logged out.");
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token"
    });

    // Swagger UI me Bearer token pass karne ke liye requirement
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer", document),
            new List<string>()
        }
    });
});

var app = builder.Build();

// SuperAdmin Seeder
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider
        .GetRequiredService<SuperAdminSeeder>();

    await seeder.SeedAsync();
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        "/swagger/v1/swagger.json",
        "My API v1");

    options.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseCors("Angular");

// Middleware order: Pehle Authentication, fir Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();