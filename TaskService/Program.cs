using Common;
using Core.Interfaces;
using Core.DataBaseContext;
using Data.Services;

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

builder.Services.AddSingleton(new MessageBroker("localhost"));
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ISubtaskService, SubtaskService>();

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
