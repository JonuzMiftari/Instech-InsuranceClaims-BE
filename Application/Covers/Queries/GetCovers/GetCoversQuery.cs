using Application.Common.Interfaces;
using Application.Covers.Dto;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Covers.Queries.GetCovers;

public record GetCoversQuery : IRequest<IEnumerable<CoverDto>>;

public class GetCoversQueryHandler : IRequestHandler<GetCoversQuery, IEnumerable<CoverDto>>
{
    private readonly IClaimsDbContext _claimsDbContext;
    private readonly IMapper _mapper;

    public GetCoversQueryHandler(IClaimsDbContext claimsDbContext, IMapper mapper)
    {
        _claimsDbContext = claimsDbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CoverDto>> Handle(GetCoversQuery request, CancellationToken cancellationToken)
        => _mapper.Map<IEnumerable<CoverDto>>(await _claimsDbContext.Covers.ToListAsync(cancellationToken));
}
