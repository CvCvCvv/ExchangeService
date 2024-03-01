using ExchangeService.Client.Abstractions.Models;
using ExchangeService.Client.Common;
using ExchangeService.Client.Requests.DirectionsExchange;
using RestSharp;

namespace ExchangeService.Client.Client;

public class DirectionsExchangeClient
{
    private readonly IRestClient _client;

    public DirectionsExchangeClient(IRestClient client)
    {
        _client = client;
    }

    public async Task<BaseResponse<Guid>> CreateDirectionExchange(string name, DateTime dateStart)
    {
        var request = new CreateDirectionExchangeRequest(name, dateStart);

        var response = await new BaseSendRequest<Guid>().SendRequest(_client, request);

        return response;
    }

    public async Task<BaseResponse<Guid>> DeleteDirectionExchange(Guid directionExchangeId)
    {
        var request = new DeleteDirectionExchangeRequest(directionExchangeId);

        var response = await new BaseSendRequest<Guid>().SendRequest(_client, request);

        return response;
    }

    public async Task<BaseResponse<DirectionExchange>> GetDirectionExchangeById(Guid directionExchangeId)
    {
        var request = new GetDirectionExchangeRequest(directionExchangeId);

        var response = await new BaseSendRequest<DirectionExchange>().SendRequest(_client, request);

        return response;
    }

    public async Task<BaseResponse<List<DirectionExchange>>> GetDirectionsExchange(string? name, DateTime? dateStart)
    {
        var request = new  GetDirectionsExchangeRequest(name, dateStart);

        var response = await new BaseSendRequest<List<DirectionExchange>>().SendRequest(_client, request);

        return response;
    }

    public async Task<BaseResponse<DirectionExchange>> UpdateDirectionExchange(Guid id, string name, DateTime dateStart)
    {
        var request = new UpdateDirectionExchangeRequest(id, name, dateStart);

        var response = await new BaseSendRequest<DirectionExchange>().SendRequest(_client, request);

        return response;
    }
}
