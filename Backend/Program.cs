using Backend;
using Backend.Data;
using Backend.Entities;
using Backend.Models;
using Backend.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantAPI.Middleware;
using Scalar.AspNetCore;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//Authentication
var authenticationSettings = new AuthenticationSettings();

builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
    };
});

//Authorization

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

//Hashers
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddScoped<Seeder>();

//Services
builder.Services.AddScoped<IAdmissionDocumentService, AdmissionDocumentService>();
builder.Services.AddScoped<IContractorService, ContractorService>();
builder.Services.AddScoped<IDocumentPositionService, DocumentPositionService>();
builder.Services.AddScoped<IAccountService, AccountService>();

//Middleware
builder.Services.AddScoped<ErrorHandlingMiddleware>();

var app = builder.Build();

//seeding
using (var scope = app.Services.CreateScope())
{
    var restaurantSeeder = scope.ServiceProvider.GetService<Seeder>();
    restaurantSeeder?.Seed();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
