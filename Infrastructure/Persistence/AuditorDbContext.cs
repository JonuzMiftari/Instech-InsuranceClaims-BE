using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;
public class AuditorDbContext : DbContext, IAuditorDbContext
{
    public AuditorDbContext(DbContextOptions<AuditorDbContext> options) :base(options) { }

    public DbSet<ClaimAudit> ClaimAudits => Set<ClaimAudit>();
    public DbSet<CoverAudit> CoverAudits => Set<CoverAudit>();

    public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken) 
        => await base.SaveChangesAsync(cancellationToken);
}
