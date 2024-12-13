using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Common
{
    public class MessageBroker
    {
        private readonly string _hostName;

        public MessageBroker(string hostName)
        {
            _hostName = hostName;
        }

        public void Publish(string queueName, string message)
        {
            var factory = new ConnectionFactory() { HostName = _hostName };

            // Создаем подключение и канал
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            // Объявляем очередь
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            // Публикуем сообщение
            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);

            Console.WriteLine($"[Broker] Отправлено сообщение: {message} в очередь: {queueName}");
        }

        public void Subscribe(string queueName, Action<string> onMessageReceived)
        {
            var factory = new ConnectionFactory() { HostName = _hostName };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            // Объявляем очередь
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            // Создаем подписчика
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"[Broker] Получено сообщение из очереди {queueName}: {message}");
                onMessageReceived(message);
            };

            // Начинаем прослушивание очереди
            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }
    }
}
