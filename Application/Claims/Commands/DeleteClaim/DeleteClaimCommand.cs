using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Claims.Commands.DeleteClaim;

public record DeleteClaimCommand(string Id) : IRequest;

public class DeleteClaimCommandHandler : IRequestHandler<DeleteClaimCommand>
{
    private readonly IClaimsDbContext _claimsDbContext;

    public DeleteClaimCommandHandler(IClaimsDbContext claimsDbContext)
    {
        _claimsDbContext = claimsDbContext;
    }
    
    public async Task Handle(DeleteClaimCommand request, CancellationToken cancellationToken)
    {
        var entity = await _claimsDbContext.Claims.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        // TODO: throw exception
        if (entity == null) return;

        _claimsDbContext.Claims.Remove(entity);
        await _claimsDbContext.SaveChangesAsync(cancellationToken);
    }
}
