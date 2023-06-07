using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Covers.Commands.DeleteCover;

public record DeleteCoverCommand(string Id) : IRequest;

public class DeleteCoverCommandHandler : IRequestHandler<DeleteCoverCommand>
{
    private readonly IClaimsDbContext _claimsDbContext;

    public DeleteCoverCommandHandler(IClaimsDbContext claimsDbContext)
    {
        _claimsDbContext = claimsDbContext;
    }

    public async Task Handle(DeleteCoverCommand request, CancellationToken cancellationToken)
    {
        var entity = await _claimsDbContext.Covers.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        // TODO: throw exception
        if (entity == null) return;

        _claimsDbContext.Covers.Remove(entity);
        await _claimsDbContext.SaveChangesAsync(cancellationToken);
    }
}