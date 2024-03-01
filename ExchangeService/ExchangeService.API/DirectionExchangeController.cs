using ExchangeService.Application.Domains.Requests.DirectionsExchange;
using ExchangeService.Application.Domains.Responses.DirectionsExchange;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace ExchangeService.API;

[ApiController]
[Route("directionsExchange")]
[Produces("application/json")]
public class DirectionExchangeController : ControllerBase
{
    private readonly IMediator _mediator;

    public DirectionExchangeController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [Route("")]
    [SwaggerResponse(StatusCodes.Status200OK, "200", typeof(GetDirectionsExchangeResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "400", typeof(GetDirectionsExchangeResponse))]
    public async Task<IActionResult> GetDirectionsExchange([FromQuery] GetDirectionsExchangeRequest request)
    {
        var resp = await _mediator.Send(request);

        if (resp.Success)
        {
            return Ok(resp);
        }

        return BadRequest(resp);
    }

    [HttpGet]
    [Route("{directionExchangeId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "200", typeof(GetDirectionExchangeByIdResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "400", typeof(GetDirectionExchangeByIdResponse))]
    public async Task<IActionResult> GetDirectionExchangeById([FromRoute] Guid directionExchangeId)
    {
        var resp = await _mediator.Send(new GetDirectionExchangeRequest() { DirectionExchangeId = directionExchangeId });

        if (resp.Success)
        {
            return Ok(resp);
        }

        return BadRequest(resp);
    }

    [HttpPost]
    [Route("create")]
    [SwaggerResponse(StatusCodes.Status200OK, "200", typeof(CreateDirectionExchangeResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "400", typeof(CreateDirectionExchangeResponse))]
    public async Task<IActionResult> CreateDirectionExchange([FromBody] CreateDirectionExchangeRequest request)
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
    [SwaggerResponse(StatusCodes.Status200OK, "200", typeof(UpdateDirectionExchangeResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "400", typeof(UpdateDirectionExchangeResponse))]
    public async Task<IActionResult> UpdateDirectionExchange([FromBody] UpdateDirectionExchangeRequest request)
    {
        var resp = await _mediator.Send(request);

        if (resp.Success)
        {
            return Ok(resp);
        }

        return BadRequest(resp);
    }

    [HttpDelete]
    [Route("delete/{directionExchangeId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "200", typeof(DeleteDirectionExchangeResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "400", typeof(DeleteDirectionExchangeResponse))]
    public async Task<IActionResult> UpdateDirectionExchange(Guid directionExchangeId)
    {
        var resp = await _mediator.Send(new DeleteDirectionExchangeRequest() { DirectionExchangeId = directionExchangeId });

        if (resp.Success)
        {
            return Ok(resp);
        }

        return BadRequest(resp);
    }
}
