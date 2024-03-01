using ExchangeService.Application.Domains.Abstractions.Entities.Enums;
using ExchangeService.Application.Domains.Responses.Operations;
using MediatR;

namespace ExchangeService.Application.Domains.Requests.Operations;

public class GetOperationsRequest : IRequest<GetOperationsResponse>
{
    public Guid? DirectionOperationId { get; set; }
    public OperationType? OperationType { get; set; }
}
