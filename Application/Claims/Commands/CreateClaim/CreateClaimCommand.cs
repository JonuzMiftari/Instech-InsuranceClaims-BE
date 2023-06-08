using Application.Claims.Dto;
using Application.Common.Interfaces;
using Application.MessagingContracts;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

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
        private readonly IMessagePublisher _messagePublisher;
        private readonly ILogger<CreateClaimCommandHandler> _logger;

        public CreateClaimCommandHandler(
            IClaimsDbContext claimsDbContext, 
            IMapper mapper, 
            IMessagePublisher messagePublisher,
            ILogger<CreateClaimCommandHandler> logger)
    {
        _messagePublisher = messagePublisher;
        _logger = logger;
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

        _logger.LogInformation("Claim created with ID: {ClaimId}", entity.Id);

        await _messagePublisher.Publish(new ClaimCreated(entity.Id), cancellationToken);
        
        return _mapper.Map<ClaimDto>(entity);
    }
}