using RestSharp;

namespace ExchangeService.Client.Client;

public class ExchangeServiceClient
{
    public ExchangesClient Exchanges { get; set; }
    public OperationsClient Operations { get; set; }
    public DirectionsExchangeClient DirectionsExchange { get; set; }
    public DirectionsOperationClient DirectionsOperation { get; set; }

    public ExchangeServiceClient(string baseUrl)
    {
        IRestClient client = new RestClient(baseUrl);
        Exchanges = new ExchangesClient(client);
        Operations = new OperationsClient(client);
        DirectionsOperation = new DirectionsOperationClient(client);
        DirectionsExchange = new DirectionsExchangeClient(client);
    }
}