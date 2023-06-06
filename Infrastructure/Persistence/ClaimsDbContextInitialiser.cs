using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;
public class ClaimsDbContextInitialiser
{
    private readonly ILogger<ClaimsDbContextInitialiser> _logger;
    private readonly ClaimsDbContext _claimsDbContext;

    public ClaimsDbContextInitialiser(ILogger<ClaimsDbContextInitialiser> logger,
        ClaimsDbContext claimsDbContext)
    {
        _logger = logger;
        _claimsDbContext = claimsDbContext;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_claimsDbContext.Database.IsCosmos())
            {
                await _claimsDbContext.Database.EnsureCreatedAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
}
