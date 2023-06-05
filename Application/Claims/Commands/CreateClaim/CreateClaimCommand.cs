using Application.Claims.Queries.GetClaims;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Claims.Commands.CreateClaim;

public record CreateClaimCommand : IRequest
{
    public string Id { get; set; }

    public string CoverId { get; set; }

    public DateTime Created { get; set; }

    public string Name { get; set; }

    public ClaimTypeDto Type { get; set; }

    public decimal DamageCost { get; set; }
}

public class CreateClaimCommandHandler : IRequestHandler<CreateClaimCommand>
{
    private readonly IClaimsRepository _claimsRepository;
    private readonly IMapper _mapper;
    public CreateClaimCommandHandler(IClaimsRepository claimsRepository, IMapper mapper)
    {
        _claimsRepository = claimsRepository;
        _mapper = mapper;
    }

    public async Task Handle(CreateClaimCommand request, CancellationToken cancellationToken)
    {
        var claim = new Claim
        {
            Id = request.Id,
            CoverId = request.CoverId,
            Created = request.Created,
            DamageCost = request.DamageCost,
            Name = request.Name,
            Type = (ClaimType) request.Type
        };

        await _claimsRepository.AddAsync(claim);
    }
}