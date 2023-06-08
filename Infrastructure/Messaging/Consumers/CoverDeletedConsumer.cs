using Application.Common.Interfaces;
using Application.MessagingContracts;
using Domain.Entities;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Messaging.Consumers;
public class CoverDeletedConsumer : IConsumer<CoverDeleted>
{
    private readonly IAuditorDbContext _dbContext;
    private readonly ILogger<CoverDeletedConsumer> _logger;

    public CoverDeletedConsumer(IAuditorDbContext dbContext, ILogger<CoverDeletedConsumer> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CoverDeleted> context)
    {
        var entity = new CoverAudit
        {
            CoverId = context.Message.Id,
            Created = DateTime.Now,
            HttpRequestType = HttpRequestType.Delete
        };

        await _dbContext.CoverAudits.AddAsync(entity);
        await _dbContext.SaveChangesAsync(new CancellationToken());

        _logger.LogInformation("Audit for deleted Cover with ID: {ClaimId}", context.Message.Id);
    }
}
