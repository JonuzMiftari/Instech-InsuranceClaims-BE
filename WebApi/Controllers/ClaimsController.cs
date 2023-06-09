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

    /// <summary>
    /// Gets all Covers
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ClaimDto>>> GetAsync()
        => Ok(await Mediator.Send(new GetClaimsQuery()));

    /// <summary>
    /// Gets a Claim
    /// </summary>
    /// <param name="id">Id of the Claim</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ClaimDto> GetAsync(string id) 
        => await Mediator.Send(new GetClaimByIdQuery(id));

    /// <summary>
    /// Creates a Claim
    /// </summary>
    ///     /// <remarks>
    /// Sample request:
    ///
    ///     {
    ///         "coverId": "guid",
    ///         "created": "2023-06-09T17:00:20.959Z",
    ///         "name": "string",
    ///         "type": "Collision",
    ///         "damageCost": 0
    ///     }
    /// </remarks>
    /// <param name="createClaimCommand">CreateClaimCommand</param>
    /// <returns>A newly created Claim</returns>
    /// <response code="201">Returns a newly created Claim</response>
    /// <response code="401">If the item is null</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClaimDto>> CreateAsync(CreateClaimCommand createClaimCommand) 
        => Ok(await Mediator.Send(createClaimCommand));

    /// <summary>
    /// Deletes a specific Cover
    /// </summary>
    /// <param name="id">Id of the Claim to delete</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task DeleteAsync(string id)
        => await Mediator.Send(new DeleteClaimCommand(id));
}