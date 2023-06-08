using Application.Common.Interfaces;
using Application.MessagingContracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Claims.Commands.DeleteClaim;

public record DeleteClaimCommand(string Id) : IRequest;

public class DeleteClaimCommandHandler : IRequestHandler<DeleteClaimCommand>
{
    private readonly IClaimsDbContext _claimsDbContext;
    private readonly IMessagePublisher _messagePublisher;
    private readonly ILogger _logger;

    public DeleteClaimCommandHandler(
        IClaimsDbContext claimsDbContext,
        IMessagePublisher messagePublisher,
        ILogger logger)
    {
        _claimsDbContext = claimsDbContext;
        _messagePublisher = messagePublisher;
        _logger = logger;
    }
    
    public async Task Handle(DeleteClaimCommand request, CancellationToken cancellationToken)
    {
        var entity = await _claimsDbContext.Claims.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        // TODO: throw exception

        if (entity == null) return;

        _claimsDbContext.Claims.Remove(entity);
        await _claimsDbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Claim deleted with ID: {ClaimId}", request.Id);

        await _messagePublisher.Publish(new ClaimDeleted(entity.Id), cancellationToken);
    }
}
