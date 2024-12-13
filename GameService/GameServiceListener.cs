using Common;
using Core.Interfaces;

namespace GameService
{
    public class GameServiceListener : BackgroundService
    {
        private readonly MessageBroker _broker;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IGamificationService _gameService;

        // Используем IServiceScopeFactory для создания нового scope
        public GameServiceListener(IGamificationService gameService, IServiceScopeFactory serviceScopeFactory)
        {
            _gameService = gameService;
            _serviceScopeFactory = serviceScopeFactory;
            _broker = new MessageBroker("localhost");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Подписка на очередь сообщений
            _broker.Subscribe("task.completed", OnTaskCompleted);
            return Task.CompletedTask;
        }

        private void OnTaskCompleted(string message)
        {
            // Получение данных из сообщения
            var taskId = int.Parse(message);
            _gameService.AddPointsAsync(taskId, 10);
            Console.WriteLine($"[GameService] Начисление 10 очков за выполнение задачи с ID {taskId}");
        }
    }
}
