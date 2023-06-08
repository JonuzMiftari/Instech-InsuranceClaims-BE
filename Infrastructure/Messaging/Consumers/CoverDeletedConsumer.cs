using Application.Common.Interfaces;
using Application.MessagingContracts;
using Domain.Entities;
using MassTransit;

namespace Infrastructure.Messaging.Consumers;
public class CoverDeletedConsumer : IConsumer<CoverDeleted>
{
    private readonly IAuditorDbContext _dbContext;

    public CoverDeletedConsumer(IAuditorDbContext dbContext)
    {
        _dbContext = dbContext;
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
    }
}
