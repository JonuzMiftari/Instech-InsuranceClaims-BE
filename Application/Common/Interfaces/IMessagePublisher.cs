namespace Application.Common.Interfaces;
public interface IMessagePublisher
{
    Task Publish(object message, CancellationToken cancellationToken = default);
}
