using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Claims.Queries.GetClaims;

public record GetClaimByIdQuery(string Id) : IRequest<ClaimDto>;

public class GetClaimByIdQueryHandler : IRequestHandler<GetClaimByIdQuery, ClaimDto>
{
    private readonly IClaimsRepo _claimsRepo;
    private readonly IMapper _mapper;

    public GetClaimByIdQueryHandler(IClaimsRepo claimsRepo, IMapper mapper)
    {
        _claimsRepo = claimsRepo;
        _mapper = mapper;
    }

    public async Task<ClaimDto> Handle(GetClaimByIdQuery request, CancellationToken cancellationToken)
    {
        var claim = await _claimsRepo.GetByIdAsync(request.Id);
        var claimDto = _mapper.Map<ClaimDto>(claim);
        return await Task.FromResult(claimDto);
    }
}