using Application.Common.Interfaces;
using MassTransit;

namespace Infrastructure.Messaging;
public class MessagePublisher : IMessagePublisher
{
    private readonly IBus _bus;
    
    // TODO: clean up commented code
    // private readonly IPublishEndpoint _publishEndpoint;

    public MessagePublisher(IBus bus, IPublishEndpoint publishEndpoint)
    {
        _bus = bus;
        //_publishEndpoint = publishEndpoint;
        //_publishEndpoint.Publish
    }

    public async Task Publish(object message, CancellationToken cancellationToken = default) 
        => await _bus.Publish(message, cancellationToken);
}