using ExchangeService.Client.Abstractions.Models;
using ExchangeService.Client.Common;
using ExchangeService.Client.Requests.DirectionsOperation;
using RestSharp;

namespace ExchangeService.Client.Client;

public class DirectionsOperationClient
{
    private readonly IRestClient _client;

    public DirectionsOperationClient(IRestClient client)
    {
        _client = client;
    }

    public async Task<BaseResponse<Guid>> CreateDirectionOperation(string name, Guid directionExchangeId)
    {
        var request = new CreateDirectionOperationRequest(name, directionExchangeId);

        var response = await new BaseSendRequest<Guid>().SendRequest(_client, request);

        return response;
    }

    public async Task<BaseResponse<Guid>> DeleteDirectionOperation(Guid directionOperationId)
    {
        var request = new DeleteDirectionOperationRequest(directionOperationId);

        var response = await new BaseSendRequest<Guid>().SendRequest(_client, request);

        return response;
    }

    public async Task<BaseResponse<DirectionOperation>> GetDirectionOperationById(Guid directionOperation)
    {
        var request = new GetDirectionOperationRequest(directionOperation);

        var response = await new BaseSendRequest<DirectionOperation>().SendRequest(_client, request);

        return response;
    }

    public async Task<BaseResponse<List<DirectionOperation>>> GetDirectionsOperation(string? name, Guid? directionExchangeId)
    {
        var request = new GetDirectionsOperationRequest(name, directionExchangeId);

        var response = await new BaseSendRequest<List<DirectionOperation>>().SendRequest(_client, request);

        return response;
    }

    public async Task<BaseResponse<DirectionOperation>> UpdateDirectionOperation(Guid id, string name, Guid directionExchangeId)
    {
        var request = new UpdateDirectionOperationRequest(id, name, directionExchangeId);

        var response = await new BaseSendRequest<DirectionOperation>().SendRequest(_client, request);

        return response;
    }
}
