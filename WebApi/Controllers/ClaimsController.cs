using Application.Claims.Commands.CreateClaim;
using Application.Claims.Commands.DeleteClaim;
using Application.Claims.Queries.GetClaims;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class ClaimsController : ApiControllerBase
{
    private readonly ILogger<ClaimsController> _logger;

    public ClaimsController(ILogger<ClaimsController> logger) 
        => _logger = logger;

    [HttpGet]
    public Task<IEnumerable<ClaimDto>> GetAsync() 
        => Mediator.Send(new GetClaimsQuery());

    [HttpGet("{id}")]
    public Task<ClaimDto> GetAsync(string id) 
        => Mediator.Send(new GetClaimByIdQuery(id));

    [HttpPost]
    public async Task<ActionResult<ClaimDto>> CreateAsync(CreateClaimCommand command) 
        => Ok(await Mediator.Send(command));

    [HttpDelete("{id}")]
    public Task DeleteAsync(string id) 
        => Mediator.Send(new DeleteClaimCommand(id));
}