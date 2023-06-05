namespace Domain.Common;
public abstract class AuditorBaseEntity : BaseEntity
{
    public DateTime Created { get; set; }

    public string? HttpRequestType { get; set; }
}

