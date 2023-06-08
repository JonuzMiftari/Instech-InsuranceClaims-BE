using Application.Common.Interfaces;
using MassTransit;

namespace Infrastructure.Messaging;
public class MessagePublisher : IMessagePublisher
{
    private readonly IBus _bus;

    public MessagePublisher(IBus bus)
    {
        _bus = bus;
    }

    public async Task Publish(object message, CancellationToken cancellationToken = default) 
        => await _bus.Publish(message, cancellationToken);
}