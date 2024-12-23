using Common;
using Core.Interfaces;

namespace GameService
{
    public class GameServiceListener : BackgroundService
    {
        private readonly MessageBroker _broker;
        private readonly IServiceProvider _services;

        public GameServiceListener(IServiceProvider services)
        {
            _services = services;
            _broker = new MessageBroker("localhost");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Подписываемся на очередь сообщений
            _broker.Subscribe("task.completed", OnTaskCompleted);

            Console.WriteLine("[GameService] Listener запущен и подписан на 'task.completed'.");
            return Task.CompletedTask;
        }

        private void OnTaskCompleted(string message)
        {
            using (var scope = _services.CreateScope())
            {
                var provider = scope.ServiceProvider;

                // Получаем зависимость AppDbContext из созданного scope
                var dbContext = provider.GetRequiredService<Core.DataBaseContext.AppDbContext>();
                var gamificationService = provider.GetRequiredService<IGamificationService>();

                // Обработка сообщения
                var taskId = int.Parse(message);
                gamificationService.AddPointsAsync(taskId, 10).GetAwaiter().GetResult(); // Синхронный вызов асинхронного метода

                Console.WriteLine($"[GameService] Начисление 10 очков за выполнение задачи с ID {taskId}");
            }
        }
    }
}
