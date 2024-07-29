using Cola.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cola.WebApi;

public static class WebApiInject
{
    /// <summary>
    ///     inject SnowFlake
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    /// <param name="configuration">configuration</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddSingletonColaWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddColaHttpClient(configuration);
        return InjectColaWebApi(services);
    }

    private static IServiceCollection InjectColaWebApi(IServiceCollection services)
    {
        services.AddSingleton<IWebApi, WebApi>();
        var colaConsole = services.BuildServiceProvider().GetService<IColaConsole>();
        colaConsole!.WriteInfo("注入类型【 IWebApi, WebApi 】");
        return services;
    }
}
