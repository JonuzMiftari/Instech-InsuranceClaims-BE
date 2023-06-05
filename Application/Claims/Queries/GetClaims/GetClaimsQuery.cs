using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Claims.Queries.GetClaims;

public record GetClaimsQuery : IRequest<IEnumerable<ClaimDto>>;

public class GetClaimsQueryHandler : IRequestHandler<GetClaimsQuery, IEnumerable<ClaimDto>>
{
    private readonly IClaimsRepository _claimsRepository;
    private readonly IMapper _mapper;

    public GetClaimsQueryHandler(IClaimsRepository claimsRepository, IMapper mapper)
    {
        _claimsRepository = claimsRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ClaimDto>> Handle(GetClaimsQuery request, CancellationToken cancellationToken)
    {
        var claims = await _claimsRepository.GetClaimsAsync();
        return _mapper.Map<IEnumerable<ClaimDto>>(claims);

        // TODO: Remove the commented code
        //var claimsDto = claims.Select(x => new ClaimDto
        //{
        //    Id = x.Id, 
        //    CoverId = x.CoverId, 
        //    Created = x.Created, 
        //    DamageCost = x.DamageCost, 
        //    Name = x.Name,
        //    Type = (ClaimTypeDto) x.Type
        //});

        //return claimsDto;
    }
}
