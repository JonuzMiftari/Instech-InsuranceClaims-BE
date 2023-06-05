namespace Application.Claims.Queries.GetClaims;

public class ClaimDto
{
    public string Id { get; set; }

    public string CoverId { get; set; }

    public DateTime Created { get; set; }

    public string Name { get; set; }

    public ClaimTypeDto Type { get; set; }

    public decimal DamageCost { get; set; }
}