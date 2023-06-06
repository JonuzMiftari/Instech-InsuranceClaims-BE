using Domain.Common;

namespace Domain.Entities;

public class CoverAudit : AuditorBaseEntity
{
    public string? CoverId { get; set; }
}
