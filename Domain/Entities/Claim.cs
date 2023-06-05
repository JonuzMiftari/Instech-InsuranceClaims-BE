﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Domain.Entities;
[JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
public class Claim
{
    public string Id { get; set; }

    public string CoverId { get; set; }

    public DateTime Created { get; set; }

    public string Name { get; set; }

    public ClaimType Type { get; set; }

    public decimal DamageCost { get; set; }

}