using Application.Interfaces;
using Application.Mappers;
using Application.Middleware;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<PhotoStockDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"))
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<DbSeeder>();
///builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton(PhotoStockMappingProfile.Initialize());

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
builder.Services.AddScoped<ITextService, TextService>();
builder.Services.AddScoped<ITextRepository, TextRepository>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
