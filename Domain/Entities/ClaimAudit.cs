using Domain.Common;

namespace Domain.Entities;
public class ClaimAudit : AuditorBaseEntity
{
    public string? ClaimId { get; set; }
}
