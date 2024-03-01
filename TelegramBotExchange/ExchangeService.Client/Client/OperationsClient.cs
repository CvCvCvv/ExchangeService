using ExchangeService.Client.Abstractions.Models;
using ExchangeService.Client.Common;
using ExchangeService.Client.Requests.Enums;
using ExchangeService.Client.Requests.Operations;
using RestSharp;

namespace ExchangeService.Client.Client;

public class OperationsClient
{
    private readonly IRestClient _client;

    public OperationsClient(IRestClient client)
    {
        _client = client;
    }

    public async Task<BaseResponse<Guid>> CreateOperation(Guid directionOperationId, Guid exchangeId, decimal sum, decimal volume, OperationTypeEnum operationType)
    {
        var request = new CreateOperationRequest(directionOperationId, exchangeId, sum, volume, operationType);

        var response = await new BaseSendRequest<Guid>().SendRequest(_client, request);

        return response;
    }

    public async Task<BaseResponse<Guid>> DeleteOperation(Guid operationId)
    {
        var request = new DeleteOperationRequest(operationId);

        var response = await new BaseSendRequest<Guid>().SendRequest(_client, request);

        return response;
    }

    public async Task<BaseResponse<Operation>> GetOperationById(Guid operationId)
    {
        var request = new GetOperationRequest(operationId);

        var response = await new BaseSendRequest<Operation>().SendRequest(_client, request);

        return response;
    }

    public async Task<BaseResponse<List<Operation>>> GetOperations(Guid? directionOperationId, Guid? exchangeId, decimal? sum, decimal? volume, OperationTypeEnum? operationType)
    {
        var request = new GetOperationsRequest(directionOperationId, exchangeId, sum, volume, operationType);

        var response = await new BaseSendRequest<List<Operation>>().SendRequest(_client, request);

        return response;
    }

    public async Task<BaseResponse<Operation>> UpdateOperation(Guid id, Guid directionOperationId, Guid exchangeId, decimal sum, decimal volume, OperationTypeEnum operationType)
    {
        var request = new UpdateOperationRequest(id, directionOperationId, exchangeId, sum, volume, operationType);

        var response = await new BaseSendRequest<Operation>().SendRequest(_client, request);

        return response;
    }
}
