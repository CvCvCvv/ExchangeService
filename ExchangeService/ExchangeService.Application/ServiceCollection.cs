using Microsoft.Extensions.DependencyInjection;

namespace ExchangeService.Application;

public static class ServiceCollection
{
    public static void AddAplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollection).Assembly));
    }
}
