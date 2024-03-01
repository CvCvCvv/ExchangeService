using ExchangeService.Client.Abstractions.Models;
using ExchangeService.Client.Common;
using ExchangeService.Client.Requests.Exchanges;
using RestSharp;

namespace ExchangeService.Client.Client;

public class ExchangesClient
{
    private readonly IRestClient _client;

    public ExchangesClient(IRestClient restClient)
    {
        _client = restClient;
    }

    public async Task<BaseResponse<Guid>> CreateExchange(Guid directionExchangeId, DateTime dateStart, DateTime dateEnd, string nameExecutor, string symbol, bool closed)
    {
        var request = new CreateExchangeRequest(directionExchangeId, dateStart, dateEnd, nameExecutor, symbol, closed);

        var response = await new BaseSendRequest<Guid>().SendRequest(_client, request);

        return response;
    }

    public async Task<BaseResponse<Guid>> DeleteExchange(Guid exchangeId)
    {
        var request = new DeleteExchangeRequest(exchangeId);

        var response = await new BaseSendRequest<Guid>().SendRequest(_client, request);

        return response;
    }

    public async Task<BaseResponse<Exchange>> GetExchangeById(Guid exchangeId)
    {
        var request = new GetExchangeRequest(exchangeId);

        var response = await new BaseSendRequest<Exchange>().SendRequest(_client, request);

        return response;
    }

    public async Task<BaseResponse<List<Exchange>>> GetExchanges(Guid? durectionExchangeId, DateTime? dateStart, DateTime? dateEnd, string? nameExecutor, string? symbol, bool? closed)
    {
        var request = new GetExchangesRequest(durectionExchangeId, dateStart, dateEnd, nameExecutor, symbol, closed);

        var response = await new BaseSendRequest<List<Exchange>>().SendRequest(_client, request);

        return response;
    }

    public async Task<BaseResponse<Exchange>> UpdateExchange(Guid id, Guid durectionExchangeId, DateTime dateStart, DateTime dateEnd, string nameExecutor, string symbol, bool closed)
    {
        var request = new UpdateExchangeRequest(id, durectionExchangeId, dateStart, dateEnd, nameExecutor, symbol, closed);

        var response = await new BaseSendRequest<Exchange>().SendRequest(_client, request);

        return response;
    }
}
