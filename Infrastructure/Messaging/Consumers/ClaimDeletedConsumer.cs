using Application.Common.Interfaces;
using Application.MessagingContracts;
using Domain.Entities;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Messaging.Consumers;
public class ClaimDeletedConsumer : IConsumer<ClaimDeleted>
{
    private readonly IAuditorDbContext _dbContext;
    private readonly ILogger _logger;

    public ClaimDeletedConsumer(IAuditorDbContext dbContext, ILogger logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ClaimDeleted> context)
    {
        var entity = new ClaimAudit
        {
            ClaimId = context.Message.Id,
            Created = DateTime.Now,
            HttpRequestType = HttpRequestType.Delete
        };

        await _dbContext.ClaimAudits.AddAsync(entity);
        await _dbContext.SaveChangesAsync(new CancellationToken());

        _logger.LogInformation("Audit for deleted Claim with ID: {ClaimId}", context.Message.Id);
    }
}
