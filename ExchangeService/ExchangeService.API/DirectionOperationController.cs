using ExchangeService.Application.Domains.Requests.DirectionsOperation;
using ExchangeService.Application.Domains.Responses.DirectionsOperation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace ExchangeService.API;

[ApiController]
[Route("directionOperation")]
[Produces("application/json")]
public class DirectionOperationController : ControllerBase
{
    private readonly IMediator _mediator;

    public DirectionOperationController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [Route("")]
    [SwaggerResponse(StatusCodes.Status200OK, "200", typeof(GetDirectionsOperationResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "400", typeof(GetDirectionsOperationResponse))]
    public async Task<IActionResult> GetDirectionsOperation([FromQuery] GetDirectionsOperationRequest request)
    {
        var resp = await _mediator.Send(request);

        if (resp.Success)
        {
            return Ok(resp);
        }

        return BadRequest(resp);
    }

    [HttpGet]
    [Route("{directionOperationId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "200", typeof(GetDirectionOperationByIdResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "400", typeof(GetDirectionOperationByIdResponse))]
    public async Task<IActionResult> GetDirectionOperationById([FromRoute] Guid directionOperationId)
    {
        var resp = await _mediator.Send(new GetDirectionOperationRequest() { DirectionOperationId = directionOperationId });

        if (resp.Success)
        {
            return Ok(resp);
        }

        return BadRequest(resp);
    }

    [HttpPost]
    [Route("create")]
    [SwaggerResponse(StatusCodes.Status200OK, "200", typeof(CreateDirectionOperationResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "400", typeof(CreateDirectionOperationResponse))]
    public async Task<IActionResult> CreateDirectionOperation([FromBody] CreateDirectionOperationRequest request)
    {
        var resp = await _mediator.Send(request);

        if (resp.Success)
        {
            return Ok(resp);
        }

        return BadRequest(resp);
    }

    [HttpPut]
    [Route("update")]
    [SwaggerResponse(StatusCodes.Status200OK, "200", typeof(UpdateDirectionOperationResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "400", typeof(UpdateDirectionOperationResponse))]
    public async Task<IActionResult> UpdateDirectionOperation([FromBody] UpdateDirectionOperationRequest request)
    {
        var resp = await _mediator.Send(request);

        if (resp.Success)
        {
            return Ok(resp);
        }

        return BadRequest(resp);
    }

    [HttpDelete]
    [Route("delete/{directionOperationId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "200", typeof(DeleteDirectionOperationResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "400", typeof(DeleteDirectionOperationResponse))]
    public async Task<IActionResult> UpdateDirectionOperation(Guid directionOperationId)
    {
        var resp = await _mediator.Send(new DeleteDirectionOperationRequest() { DirectionOperationId = directionOperationId });

        if (resp.Success)
        {
            return Ok(resp);
        }

        return BadRequest(resp);
    }
}
