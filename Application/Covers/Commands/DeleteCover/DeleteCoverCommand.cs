using Application.Common.Interfaces;
using Application.MessagingContracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Covers.Commands.DeleteCover;

public record DeleteCoverCommand(string Id) : IRequest;

public class DeleteCoverCommandHandler : IRequestHandler<DeleteCoverCommand>
{
    private readonly IClaimsDbContext _claimsDbContext;
    private readonly IMessagePublisher _messagePublisher;
    private readonly ILogger _logger;

    public DeleteCoverCommandHandler(IClaimsDbContext claimsDbContext, IMessagePublisher messagePublisher, ILogger logger)
    {
        _claimsDbContext = claimsDbContext;
        _messagePublisher = messagePublisher;
        _logger = logger;
    }

    public async Task Handle(DeleteCoverCommand request, CancellationToken cancellationToken)
    {
        var entity = await _claimsDbContext.Covers.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        // TODO: throw exception
        if (entity == null) return;

        _claimsDbContext.Covers.Remove(entity);
        await _claimsDbContext.SaveChangesAsync(cancellationToken);

        await _messagePublisher.Publish(new CoverDeleted(entity.Id), cancellationToken);

        _logger.LogInformation("Cover deleted with id: {ClaimId}", request.Id);
    }
}