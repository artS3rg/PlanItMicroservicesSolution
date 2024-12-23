using Core.DataBaseContext;
using Core.Interfaces;
using Data.Services;
using StatisticsService;

var builder = WebApplication.CreateBuilder(args);

// Настройка логирования
builder.Logging
    .ClearProviders()  // Убирает все провайдеры логирования
    .AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddHostedService<StatisticsServiceListener>();
builder.Services.AddScoped<IStatisticService, StatisticService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
