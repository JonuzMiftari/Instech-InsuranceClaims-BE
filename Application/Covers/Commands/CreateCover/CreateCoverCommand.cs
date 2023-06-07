using Application.Common.Interfaces;
using Application.Covers.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Covers.Commands.CreateCover;

public record CreateCoverCommand : IRequest<CoverDto>
{
    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public CoverTypeDto Type { get; set; }

    public decimal Premium { get; set; }
}

public class CreateCoverCommandHandler : IRequestHandler<CreateCoverCommand, CoverDto>
{
    private readonly IClaimsDbContext _claimsDbContext;
    private readonly IMapper _mapper;

    public CreateCoverCommandHandler(IClaimsDbContext claimsDbContext, IMapper mapper)
    {
        _claimsDbContext = claimsDbContext;
        _mapper = mapper;
    }

    public async Task<CoverDto> Handle(CreateCoverCommand request, CancellationToken cancellationToken)
    {
        var entity = new Cover
        {
            Id = Guid.NewGuid().ToString(),
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Premium = request.Premium,
            Type = (CoverType) request.Type
        };

        await _claimsDbContext.Covers.AddAsync(entity, cancellationToken);
        await _claimsDbContext.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CoverDto>(entity);
    }
}