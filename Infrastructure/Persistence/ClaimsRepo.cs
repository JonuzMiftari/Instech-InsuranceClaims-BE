using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;
internal class ClaimsRepo : IClaimsRepo
{
    private readonly IClaimsDbContext _claimsDbContext;

    public ClaimsRepo(IClaimsDbContext claimsDbContext)
    {
        _claimsDbContext = claimsDbContext;
    }

    public async Task<IEnumerable<Claim>> GetClaimsAsync()
    {
        return await _claimsDbContext.Claims.ToListAsync();
    }

    public Task<Claim> GetByIdAsync(string id)
    {
        return _claimsDbContext.Claims.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Claim entity)
    {
        await _claimsDbContext.Claims.AddAsync(entity);
        await _claimsDbContext.SaveChangesAsync(new CancellationToken());
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await GetByIdAsync(id);
        _claimsDbContext.Claims.Remove(entity);
        await _claimsDbContext.SaveChangesAsync(new CancellationToken());
    }
}
