using Application.Covers.Commands.CreateCover;
using Application.Covers.Commands.DeleteCover;
using Application.Covers.Dto;
using Application.Covers.Queries.GetCovers;
using Application.Premiums.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class CoversController : ApiControllerBase
{
    /// <summary>
    /// Computes Premiums for defined period and Cover Type
    /// </summary>
    /// <param name="startDate">Start Date</param>
    /// <param name="endDate">End Date</param>
    /// <param name="coverType">Cover Type</param>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<decimal>> ComputePremiumAsync(DateOnly startDate, DateOnly endDate, CoverTypeDto coverType)
        => Ok(await Mediator.Send(new PremiumCalculatorQuery(startDate, endDate, coverType)));


    /// <summary>
    /// Gets all Covers
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CoverDto>>> GetAsync()
        => Ok(await Mediator.Send(new GetCoversQuery()));

    /// <summary>
    /// Gets a Cover
    /// </summary>
    /// <param name="id">Id of the Cover</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CoverDto>> GetAsync(string id)
        => await Mediator.Send(new GetCoverByIdQuery(id));

    /// <summary>
    /// Creates a Cover.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Create
    ///     {
    ///        "startDate": "2023-06-05",
    ///         "endDate": "2023-06-09",
    ///         "type": "Yacht",
    ///         "premium": 0
    ///     }
    ///
    /// </remarks>
    /// <param name="createCoverCommand">CreateCoverCommand</param>
    /// <returns>A newly created Cover</returns>
    /// <response code="201">returns a newly created Cover</response>
    /// <response code="401">If the item is null</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateAsync(CreateCoverCommand createCoverCommand)
        => Ok(await Mediator.Send(createCoverCommand));

    /// <summary>
    /// Deletes a specific Cover
    /// </summary>
    /// <param name="id">Id of the Cover to delete</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task DeleteAsync(string id)
        => await Mediator.Send(new DeleteCoverCommand(id));
}