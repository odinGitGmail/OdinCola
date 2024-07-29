using Cola.Console;
using Microsoft.Extensions.DependencyInjection;
namespace Cola.Exception;

public static class ColaExceptionInject
{
    public static IServiceCollection AddColaExceptionSingleton(this IServiceCollection services)
    {
        services.AddSingleton<IColaException, ColaException>();
        var colaConsole = services.BuildServiceProvider().GetService<IColaConsole>();
        colaConsole!.WriteInfo("注入类型【 IColaException, ColaException 】");
        return services;
    }
}
