using RabbitMQ.Client;

namespace NinjaWorld.Application.Helpers
{
    internal class RabbitMqHellper
    {
        public static IModel CreateChannel(IConnection connection)
        {
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: Constants.RequestQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueDeclare(queue: Constants.ResponseQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            return channel;
        }
    }
}