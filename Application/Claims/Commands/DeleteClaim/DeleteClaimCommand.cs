using Application.Common.Interfaces;
using MediatR;

namespace Application.Claims.Commands.DeleteClaim;

public record DeleteClaimCommand(string Id) : IRequest;

public class DeleteClaimCommandHandler : IRequestHandler<DeleteClaimCommand>
{
    private readonly IClaimsRepo _claimsRepo;

    public DeleteClaimCommandHandler(IClaimsRepo claimsRepo)
    {
        _claimsRepo = claimsRepo;
    }
    
    public async Task Handle(DeleteClaimCommand request, CancellationToken cancellationToken)
    {
        await _claimsRepo.DeleteAsync(request.Id);
    }
}
