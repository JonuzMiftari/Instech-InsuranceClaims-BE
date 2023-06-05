namespace Application.Common.Interfaces;
public interface IClaimsAuditorRepository
{
    Task<int> AuditClaim(string id, string httpRequestType, CancellationToken cancellationToken);
}