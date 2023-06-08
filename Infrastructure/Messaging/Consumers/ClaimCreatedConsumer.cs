using Application.Common.Interfaces;
using Application.MessagingContracts;
using Domain.Entities;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Messaging.Consumers;
public class ClaimCreatedConsumer : IConsumer<ClaimCreated>
{
    private readonly IAuditorDbContext _dbContext;
    private readonly ILogger<ClaimCreatedConsumer> _logger;

    public ClaimCreatedConsumer(IAuditorDbContext dbContext, ILogger<ClaimCreatedConsumer> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ClaimCreated> context)
    {
        var entity = new ClaimAudit
        {
            ClaimId = context.Message.Id,
            Created = DateTime.Now,
            HttpRequestType = HttpRequestType.Post
        };

        await _dbContext.ClaimAudits.AddAsync(entity);
        await _dbContext.SaveChangesAsync(new CancellationToken());

        _logger.LogInformation("Audit for deleted Claim with ID: {ClaimId}", context.Message.Id);
    }
}