using Application.Claims.Dto;
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
    private readonly IClaimsDbContext _claimsDbContext;
    private readonly IMapper _mapper;
    public CreateClaimCommandHandler(IClaimsDbContext claimsDbContext, IMapper mapper)
    {
        _claimsDbContext = claimsDbContext;
        _mapper = mapper;
    }

    public async Task<ClaimDto> Handle(CreateClaimCommand request, CancellationToken cancellationToken)
    {
        var entity = new Claim
        {
            Id = Guid.NewGuid().ToString(),
            CoverId = request.CoverId,
            Created = request.Created,
            DamageCost = request.DamageCost,
            Name = request.Name,
            Type = (ClaimType)request.Type
        };

        await _claimsDbContext.Claims.AddAsync(entity, cancellationToken);
        await _claimsDbContext.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ClaimDto>(entity);
    }
}