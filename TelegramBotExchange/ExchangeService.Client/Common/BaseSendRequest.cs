using ExchangeService.Client.Extensions;
using RestSharp;

namespace ExchangeService.Client.Common;

internal class BaseSendRequest<T>
{
    public async Task<BaseResponse<T>> SendRequest(IRestClient client, BaseRequest baseRequest)
    {
        var method = baseRequest.Method;

        var endpoint = method == Method.Get ? baseRequest.Properties.GenerateParametersString() : string.Empty;
        var request = new RestRequest(baseRequest.EndPoint + endpoint, method);

        if (method != Method.Get && baseRequest.Properties is not null)
        {
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(baseRequest.Properties);
        }

        var response = await SendRequest(request.Method, client, request);

        if (response != null)
        {
            return response;
        }

        return new BaseResponse<T> { Success = false };
    }

    private async Task<BaseResponse<T>?> SendRequest(Method method, IRestClient client, RestRequest request)
    {
        BaseResponse<T>? result = null;
        try
        {
            switch (method)
            {
                case Method.Post:
                    result = await client.PostAsync<BaseResponse<T>>(request);
                    break;
                case Method.Put:
                    result = await client.PutAsync<BaseResponse<T>>(request);
                    break;
                case Method.Delete:
                    result = await client.DeleteAsync<BaseResponse<T>>(request);
                    break;
                default:
                    result = await client.GetAsync<BaseResponse<T>>(request);
                    break;
            }
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
        }

        return result;
    }
}
