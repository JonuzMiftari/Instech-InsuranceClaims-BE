using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Claims.Queries.GetClaims;

public class ClaimDto : IMapFrom<Claim>
{
    public string Id { get; set; }

    public string CoverId { get; set; }

    public DateTime Created { get; set; }

    public string Name { get; set; }

    public ClaimTypeDto Type { get; set; }

    public decimal DamageCost { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Claim, ClaimDto>()
            .ForMember(d => d.Type , opt => opt.MapFrom(s => (int)s.Type));
    }
}