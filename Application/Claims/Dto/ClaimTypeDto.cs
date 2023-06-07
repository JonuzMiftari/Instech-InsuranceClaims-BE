using Application.Mappings;
using Domain.Enums;

namespace Application.Claims.Dto;

public enum ClaimTypeDto
{
    Collision = 0,
    Grounding = 1,
    BadWeather = 2,
    Fire = 3
}