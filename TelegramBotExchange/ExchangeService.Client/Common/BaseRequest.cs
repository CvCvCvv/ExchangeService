using RestSharp;

namespace ExchangeService.Client.Common;

public abstract class BaseRequest
{
    internal abstract string EndPoint { get; }
    internal abstract Method Method { get; }
    internal virtual IDictionary<string, string> Properties => null!;
    public virtual IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
}
