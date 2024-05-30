namespace NinjaWorld.Application.Interfaces
{
    public interface IMessagePublisher
    {
        Task Publish<T>(T message);
    }
}
