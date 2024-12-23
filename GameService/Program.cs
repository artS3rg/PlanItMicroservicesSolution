using Core.DataBaseContext;
using Core.Interfaces;
using Game.Repositories;
using GameService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Настройка логирования
builder.Logging
    .ClearProviders()  // Убирает все провайдеры логирования
    .AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Добавляем DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHostedService<GameServiceListener>();
builder.Services.AddScoped<IGamificationService, GamificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
