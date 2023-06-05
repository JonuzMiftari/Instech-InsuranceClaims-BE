namespace Application.Common.Interfaces;
public interface ICoverAuditorRepository
{
    Task<int> AuditCover(string id, string httpRequestType, CancellationToken cancellationToken);
}