namespace ExchangeService.Client.Common;

public class BaseResponse<T>
{
    public bool Success { get; set; } = true;
    public T? Data { get; set; }
}
