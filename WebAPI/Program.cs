using Application.Dto;
using Application.Interfaces;
using Application.Mappers;
using Application.Middleware;
using Application.Services;
using Application.Validators;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var authenticationSettings = new AuthenticationSettings();

builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
    };
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<PhotoStockDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"))
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddScoped<DbSeeder>();
///builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton(PhotoStockMappingProfile.Initialize());

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
builder.Services.AddScoped<ITextService, TextService>();
builder.Services.AddScoped<ITextRepository, TextRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IValidator<ItemQuery>,AuthorQueryValidator>();
builder.Services.AddScoped<IValidator<ItemQuery>,PhotoQueryValidator>();
builder.Services.AddScoped<IValidator<ItemQuery>,TextQueryValidator>();
builder.Services.AddScoped<IValidator<RegisterUserDto>,RegisterUserValidator>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

var app = builder.Build();

using var scope = app.Services.CreateScope();

var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
seeder.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
