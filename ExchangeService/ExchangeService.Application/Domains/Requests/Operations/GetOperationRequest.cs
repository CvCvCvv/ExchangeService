using ExchangeService.Application.Domains.Responses.Operations;
using MediatR;

namespace ExchangeService.Application.Domains.Requests.Operations;

public class GetOperationRequest : IRequest<GetOperationByIdResponse>
{
    public Guid OperationId { get; set; }
}
