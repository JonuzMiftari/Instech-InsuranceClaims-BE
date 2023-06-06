using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;
public class ClaimsDbContext : DbContext, IClaimsDbContext
{
    public ClaimsDbContext(DbContextOptions<ClaimsDbContext> options)
    : base(options)
    { }

    public DbSet<Claim> Claims => Set<Claim>();

    public DbSet<Cover> Covers => Set<Cover>();

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Claim>()
            .ToContainer(nameof(Claims))
            .HasPartitionKey(e => e.Id);

        modelBuilder.Entity<Cover>()
            .ToContainer(nameof(Covers))
            .HasPartitionKey(e => e.Id);

        base.OnModelCreating(modelBuilder);
    }
}
