using ExchangeService.Application.Domains.Requests.Exchanges;
using ExchangeService.Application.Domains.Responses.Exchanges;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace ExchangeService.API;
[ApiController]
[Route("exchanges")]
[Produces("application/json")]
public class ExchangeController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExchangeController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [Route("")]
    [SwaggerResponse(StatusCodes.Status200OK, "200", typeof(GetExchangesResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "400", typeof(GetExchangesResponse))]
    public async Task<IActionResult> GetExchanges([FromQuery] GetExchangesRequest request)
    {
        var resp = await _mediator.Send(request);

        if (resp.Success)
        {
            return Ok(resp);
        }
        
        return BadRequest(resp);
    }

    [HttpGet]
    [Route("{exchangeId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "200", typeof(GetExchangeByIdResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "400", typeof(GetExchangeByIdResponse))]
    public async Task<IActionResult> GetExchangeById([FromRoute] Guid exchangeId)
    {
        var resp = await _mediator.Send(new GetExchangeRequest() { ExchangeId = exchangeId });

        if (resp.Success)
        {
            return Ok(resp);
        }

        return BadRequest(resp);
    }

    [HttpPost]
    [Route("create")]
    [SwaggerResponse(StatusCodes.Status200OK, "200", typeof(CreateExchangeResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "400", typeof(CreateExchangeResponse))]
    public async Task<IActionResult> CreateExchange([FromBody] CreateExchangeRequest request)
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
    [SwaggerResponse(StatusCodes.Status200OK, "200", typeof(UpdateExchangeResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "400", typeof(UpdateExchangeResponse))]
    public async Task<IActionResult> UpdateExchange([FromBody] UpdateExchangeRequest request)
    {
        var resp = await _mediator.Send(request);

        if (resp.Success)
        {
            return Ok(resp);
        }

        return BadRequest(resp);
    }

    [HttpDelete]
    [Route("delete/{exchangeId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "200", typeof(UpdateExchangeResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "400", typeof(UpdateExchangeResponse))]
    public async Task<IActionResult> UpdateExchange(Guid exchangeId)
    {
        var resp = await _mediator.Send(new DeleteExchangeRequest() { ExchangeId = exchangeId });

        if (resp.Success)
        {
            return Ok(resp);
        }

        return BadRequest(resp);
    }
}
