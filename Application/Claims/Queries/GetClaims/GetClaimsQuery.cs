using Application.Claims.Dto;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Claims.Queries.GetClaims;

public record GetClaimsQuery : IRequest<IEnumerable<ClaimDto>>;

public class GetClaimsQueryHandler : IRequestHandler<GetClaimsQuery, IEnumerable<ClaimDto>>
{
    private readonly IClaimsDbContext _claimsDbContext;
    private readonly IMapper _mapper;

    public GetClaimsQueryHandler(IClaimsDbContext claimsDbContext, IMapper mapper)
    {
        _claimsDbContext = claimsDbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ClaimDto>> Handle(GetClaimsQuery request, CancellationToken cancellationToken)
        => _mapper.Map<IEnumerable<ClaimDto>>(await _claimsDbContext.Claims.ToListAsync(cancellationToken));
}
