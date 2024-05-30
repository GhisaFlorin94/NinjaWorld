using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using WarResolverClient.Models;
using Microsoft.Extensions.DependencyInjection;
using WarResolverClient.Services.Interfaces;
using WarResolverClient.Helpers;
using Newtonsoft.Json;

namespace WarResolverClient.BackgroundWorkers
{
    public class WarResolverWorker : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private EventingBasicConsumer _consumer;
        private readonly IServiceScopeFactory _scopeFactory;


        public WarResolverWorker(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            var connection = factory.CreateConnection();

            _channel =  RabbitMqHelper.CreateChannel(connection);
            _consumer = new EventingBasicConsumer(_channel);
            _channel.BasicConsume(queue: Constants.RequestQueueName, autoAck: true, consumer: _consumer);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {  
            _consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine("Message Received: " + message);
                var warDeclarationRequest = JsonConvert.DeserializeObject<WarDeclarationRequest>(message, SerializerHelper.GetJsonSerializerSettings());
                using var scope = _scopeFactory.CreateScope();
                var warResolverService = scope.ServiceProvider.GetService<IWarResolverService>();
                var warResult = warResolverService.ResolveWar(warDeclarationRequest);

                var responseText = JsonConvert.SerializeObject(warResult, SerializerHelper.GetJsonSerializerSettings());
                var resultBody = Encoding.UTF8.GetBytes(responseText);
                _channel.BasicPublish(exchange: "", routingKey: Constants.ResponseQueueName, basicProperties: null, body: resultBody);
                Console.WriteLine("Message published: " + responseText);
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
