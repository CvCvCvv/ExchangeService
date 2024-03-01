using ExchangeService.Application.Domains.Responses.Operations;
using MediatR;

namespace ExchangeService.Application.Domains.Requests.Operations;

public class DeleteOperationRequest : IRequest<DeleteOperationResponse>
{
    public Guid OperationId { get; set; }
}
