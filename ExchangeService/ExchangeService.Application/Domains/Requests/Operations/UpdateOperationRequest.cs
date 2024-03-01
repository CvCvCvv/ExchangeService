using ExchangeService.Application.Domains.Abstractions.Entities.Enums;
using ExchangeService.Application.Domains.Responses.Operations;
using MediatR;

namespace ExchangeService.Application.Domains.Requests.Operations;

public class UpdateOperationRequest : IRequest<UpdateOperationResponse>
{
    public Guid Id { get; set; }
    public Guid DirectionOperationId { get; set; }
    public Guid ExchangeId { get; set; }
    public decimal Sum { get; set; }
    public decimal Volume { get; set; }
    public OperationType OperationType { get; set; }
}
