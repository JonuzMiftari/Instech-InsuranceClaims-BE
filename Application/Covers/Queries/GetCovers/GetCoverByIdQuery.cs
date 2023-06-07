using Application.Common.Interfaces;
using Application.Covers.Dto;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Covers.Queries.GetCovers;
public record GetCoverByIdQuery(string Id) : IRequest<CoverDto>;

public class GetCoverByIdQueryHandler : IRequestHandler<GetCoverByIdQuery, CoverDto>
{
    private readonly IClaimsDbContext _claimsDbContext;
    private readonly IMapper _mapper;

    public GetCoverByIdQueryHandler(IClaimsDbContext claimsDbContext, IMapper mapper)
    {
        _claimsDbContext = claimsDbContext;
        _mapper = mapper;
    }

    public async Task<CoverDto> Handle(GetCoverByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _claimsDbContext.Covers.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        var coverDto = _mapper.Map<CoverDto>(entity);
        return await Task.FromResult(coverDto);
    }
}