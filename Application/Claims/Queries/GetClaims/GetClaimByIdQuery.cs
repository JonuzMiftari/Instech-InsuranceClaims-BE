using Application.Claims.Dto;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Claims.Queries.GetClaims;

public record GetClaimByIdQuery(string Id) : IRequest<ClaimDto>;

public class GetClaimByIdQueryHandler : IRequestHandler<GetClaimByIdQuery, ClaimDto>
{
    private readonly IMapper _mapper;
    private readonly IClaimsDbContext _claimsDbContext;

    public GetClaimByIdQueryHandler(IClaimsDbContext claimsDbContext, IMapper mapper)
    {
        _mapper = mapper;
        _claimsDbContext = claimsDbContext;
    }

    public async Task<ClaimDto> Handle(GetClaimByIdQuery request, CancellationToken cancellationToken)
    {
        var claim  = await _claimsDbContext.Claims.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        var claimDto = _mapper.Map<ClaimDto>(claim);
        return await Task.FromResult(claimDto);
    }
}