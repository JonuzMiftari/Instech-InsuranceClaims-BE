using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Claims.Queries.GetClaims;

public record GetClaimByIdQuery(string Id) : IRequest<ClaimDto>;

public class GetClaimByIdQueryHandler : IRequestHandler<GetClaimByIdQuery, ClaimDto>
{
    private readonly IClaimsRepository _claimsRepository;
    private readonly IMapper _mapper;

    public GetClaimByIdQueryHandler(IClaimsRepository claimsRepository, IMapper mapper)
    {
        _claimsRepository = claimsRepository;
        _mapper = mapper;
    }

    public async Task<ClaimDto> Handle(GetClaimByIdQuery request, CancellationToken cancellationToken)
    {
        var claim = await _claimsRepository.GetByIdAsync(request.Id);
        var claimDto = _mapper.Map<ClaimDto>(claim);
        return await Task.FromResult(claimDto);
    }
}