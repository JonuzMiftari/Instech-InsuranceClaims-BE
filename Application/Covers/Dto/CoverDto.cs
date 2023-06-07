using Application.Mappings;
using Domain.Entities;
using Domain.Enums;

namespace Application.Covers.Dto;

public class CoverDto : IMapFrom<Cover>
{
    public string Id { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public CoverType Type { get; set; }

    public decimal Premium { get; set; }
}