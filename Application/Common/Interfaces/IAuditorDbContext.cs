using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;
public interface IAuditorDbContext
{
    DbSet<ClaimAudit> ClaimAudits { get; }

    DbSet<CoverAudit> CoverAudits { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}