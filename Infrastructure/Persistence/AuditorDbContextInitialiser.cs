using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;
public class AuditorDbContextInitialiser
{
    private readonly AuditorDbContext _context;
    private readonly ILogger<AuditorDbContextInitialiser> _logger;

    public AuditorDbContextInitialiser(AuditorDbContext context, 
        ILogger<AuditorDbContextInitialiser> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogError(ex, "An error occurred while initialising Auditor database.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising Auditor database.");
            throw;
        }
    }
}
