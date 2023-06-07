using Application.Covers.Commands.CreateCover;
using Application.Covers.Commands.DeleteCover;
using Application.Covers.Dto;
using Application.Covers.Queries.GetCovers;
using Application.Premiums.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class CoversController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<decimal>> ComputePremiumAsync(DateOnly startDate, DateOnly endDate, CoverTypeDto coverType)
        => Ok(await Mediator.Send(new PremiumCalculatorQuery(startDate, endDate, coverType)));

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CoverDto>>> GetAsync()
        => Ok(await Mediator.Send(new GetCoversQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult<CoverDto>> GetAsync(string id)
        => await Mediator.Send(new GetCoverByIdQuery(id));

    [HttpPost]
    public async Task<ActionResult> CreateAsync(CreateCoverCommand createCoverCommand)
        => Ok(await Mediator.Send(createCoverCommand));

    [HttpDelete("{id}")]
    public async Task DeleteAsync(string id)
        => await Mediator.Send(new DeleteCoverCommand(id));
}