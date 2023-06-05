using System.Drawing;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Claims.Commands.DeleteClaim;

public record DeleteClaimCommand(string Id) : IRequest;

public class DeleteClaimCommandHandler : IRequestHandler<DeleteClaimCommand>
{
    private readonly IClaimsRepository _claimsRepository;

    public DeleteClaimCommandHandler(IClaimsRepository claimsRepository)
    {
        _claimsRepository = claimsRepository;
    }
    
    public async Task Handle(DeleteClaimCommand request, CancellationToken cancellationToken)
    {
        await _claimsRepository.DeleteAsync(request.Id);
    }
}
