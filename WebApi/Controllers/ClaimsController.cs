using Application.Claims.Commands.CreateClaim;
using Application.Claims.Commands.DeleteClaim;
using Application.Claims.Dto;
using Application.Claims.Queries.GetClaims;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class ClaimsController : ApiControllerBase
{
    private readonly ILogger<ClaimsController> _logger;

    public ClaimsController(ILogger<ClaimsController> logger) 
        => _logger = logger;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClaimDto>>> GetAsync()
        => Ok(await Mediator.Send(new GetClaimsQuery()));

    [HttpGet("{id}")]
    public async Task<ClaimDto> GetAsync(string id) 
        => await Mediator.Send(new GetClaimByIdQuery(id));

    [HttpPost]
    public async Task<ActionResult<ClaimDto>> CreateAsync(CreateClaimCommand createClaimCommand) 
        => Ok(await Mediator.Send(createClaimCommand));

    [HttpDelete("{id}")]
    public async Task DeleteAsync(string id)
        => await Mediator.Send(new DeleteClaimCommand(id));
}