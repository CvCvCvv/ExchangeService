using ExchangeService.Application.Domains.Requests.Operations;
using ExchangeService.Application.Domains.Responses.Operations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace ExchangeService.API;

[ApiController]
[Route("operations")]
[Produces("application/json")]
public class OperationController : ControllerBase
{
    private readonly IMediator _mediator;

    public OperationController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [Route("")]
    [SwaggerResponse(StatusCodes.Status200OK, "200", typeof(GetOperationsResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "400", typeof(GetOperationsResponse))]
    public async Task<IActionResult> GetOperations([FromQuery] GetOperationsRequest request)
    {
        var resp = await _mediator.Send(request);

        if (resp.Success)
        {
            return Ok(resp);
        }

        return BadRequest(resp);
    }

    [HttpGet]
    [Route("{operationId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "200", typeof(GetOperationByIdResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "400", typeof(GetOperationByIdResponse))]
    public async Task<IActionResult> GetOperationById([FromRoute] Guid operationId)
    {
        var resp = await _mediator.Send(new GetOperationRequest() { OperationId = operationId });

        if (resp.Success)
        {
            return Ok(resp);
        }

        return BadRequest(resp);
    }

    [HttpPost]
    [Route("create")]
    [SwaggerResponse(StatusCodes.Status200OK, "200", typeof(CreateOperationResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "400", typeof(CreateOperationResponse))]
    public async Task<IActionResult> CreateOperation([FromBody] CreateOperationRequest request)
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
    [SwaggerResponse(StatusCodes.Status200OK, "200", typeof(UpdateOperationResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "400", typeof(UpdateOperationResponse))]
    public async Task<IActionResult> UpdateOperation([FromBody] UpdateOperationRequest request)
    {
        var resp = await _mediator.Send(request);

        if (resp.Success)
        {
            return Ok(resp);
        }

        return BadRequest(resp);
    }

    [HttpDelete]
    [Route("delete/{operationId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "200", typeof(DeleteOperationResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "400", typeof(DeleteOperationResponse))]
    public async Task<IActionResult> DeleteOperation(Guid operationId)
    {
        var resp = await _mediator.Send(new DeleteOperationRequest() { OperationId = operationId });

        if (resp.Success)
        {
            return Ok(resp);
        }

        return BadRequest(resp);
    }
}
