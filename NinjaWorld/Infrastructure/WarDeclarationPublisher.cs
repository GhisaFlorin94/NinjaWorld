using NinjaWorld.Application.Interfaces;

namespace NinjaWorld.Infrastructure
{
    public class WarDeclarationPublisher : IMessagePublisher
    {
        public Task Publish<T>(T message)
        {
            throw new NotImplementedException();
        }
    }
}
