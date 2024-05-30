using Newtonsoft.Json;
using NinjaWorld.Application.Helpers;
using NinjaWorld.Application.Interfaces;
using NinjaWorld.Application.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace NinjaWorld.Application.BackgroundWorkers
{
    public class WarAftermatchWorker : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private EventingBasicConsumer _consumer;
        private readonly IServiceScopeFactory _scopeFactory;

        public WarAftermatchWorker(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            var connection = factory.CreateConnection();

            _channel = RabbitMqHellper.CreateChannel(connection);
            _consumer = new EventingBasicConsumer(_channel);
            _channel.BasicConsume(queue: Constants.ResponseQueueName, autoAck: true, consumer: _consumer);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Received += async (model, ea) =>
            {
                var receivedMessage = Encoding.UTF8.GetString(ea.Body.ToArray());
                var warResult = JsonConvert.DeserializeObject<WarResult>(receivedMessage, SerializerHelper.GetJsonSerializerSettings());
                using var scope = _scopeFactory.CreateScope();
                var ninjaService = scope.ServiceProvider.GetService<INinjaService>();
                await ninjaService.HandleWarAftermatch(warResult);
            };

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }
}