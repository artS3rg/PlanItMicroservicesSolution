using Common;

namespace StatisticsService
{
    public class StatisticsServiceListener : BackgroundService
    {
        private readonly MessageBroker _broker;

        public StatisticsServiceListener()
        {
            _broker = new MessageBroker("localhost");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _broker.Subscribe("task.completed", OnTaskCompleted);
            _broker.Subscribe("task.updated", OnTaskUpdated);
            _broker.Subscribe("task.created", OnTaskCreated);
            _broker.Subscribe("task.deleted", OnTaskDeleted);
            _broker.Subscribe("subtask.completed", OnSubtaskCompleted);
            _broker.Subscribe("subtask.updated", OnSubtaskUpdated);
            _broker.Subscribe("subtask.created", OnSubtaskCreated);
            _broker.Subscribe("subtask.deleted", OnSubtaskDeleted);
            return Task.CompletedTask;
        }

        private void OnTaskCompleted(string message)
        {
            Console.WriteLine($"[StatisticsService] Задача завершена: {message}");
            // Возможный доп функционал
        }

        private void OnTaskUpdated(string message)
        {
            Console.WriteLine($"[StatisticsService] Обновление задачи: {message}");
        }

        private void OnTaskCreated(string message)
        {
            Console.WriteLine($"[StatisticsService] Создание задачи: {message}");
        }

        private void OnTaskDeleted(string message)
        {
            Console.WriteLine($"[StatisticsService] Удаление задачи: {message}");
        }

        private void OnSubtaskCompleted(string message)
        {
            Console.WriteLine($"[StatisticsService] Подзадача завершена: {message}");
            // Возможный доп функционал
        }

        private void OnSubtaskUpdated(string message)
        {
            Console.WriteLine($"[StatisticsService] Обновление подзадачи: {message}");
        }

        private void OnSubtaskCreated(string message)
        {
            Console.WriteLine($"[StatisticsService] Создание подзадачи: {message}");
        }

        private void OnSubtaskDeleted(string message)
        {
            Console.WriteLine($"[StatisticsService] Удаление подзадачи: {message}");
        }
    }
}
