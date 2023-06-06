namespace Domain.Common;
public abstract class AuditorBaseEntity : BaseEntity<int>
{
    public DateTime Created { get; set; }

    public string? HttpRequestType { get; set; }
}

