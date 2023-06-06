using Application.Claims.Queries.GetClaims;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Claims.Commands.CreateClaim;

public record CreateClaimCommand : IRequest<ClaimDto>
{
    public string CoverId { get; set; }

    public DateTime Created { get; set; }

    public string Name { get; set; }

    public ClaimTypeDto Type { get; set; }

    public decimal DamageCost { get; set; }
}

public class CreateClaimCommandHandler : IRequestHandler<CreateClaimCommand, ClaimDto>
{
    private readonly IClaimsRepo _claimsRepo;
    private readonly IMapper _mapper;
    public CreateClaimCommandHandler(IClaimsRepo claimsRepo, IMapper mapper)
    {
        _claimsRepo = claimsRepo;
        _mapper = mapper;
    }

    public async Task<ClaimDto> Handle(CreateClaimCommand request, CancellationToken cancellationToken)
    {
        var claim = new Claim
        {
            Id = Guid.NewGuid().ToString(),
            CoverId = request.CoverId,
            Created = request.Created,
            DamageCost = request.DamageCost,
            Name = request.Name,
            Type = (ClaimType) request.Type
        };

        await _claimsRepo.AddAsync(claim);

        return _mapper.Map<ClaimDto>(claim);
    }
}