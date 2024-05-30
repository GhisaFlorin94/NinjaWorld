using RabbitMQ.Client;

namespace WarResolverClient.Helpers
{
    internal class RabbitMqHelper
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