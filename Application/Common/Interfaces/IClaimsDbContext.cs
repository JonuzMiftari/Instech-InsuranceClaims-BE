using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IClaimsDbContext
{
    DbSet<Claim>? Claims { get; }

    DbSet<Cover>? Covers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}