using Domain.Common;
using Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Domain.Entities;

[JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
public class Cover : BaseEntity<string>
{
    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public CoverType Type { get; set; }

    public decimal Premium { get; set; }
}
