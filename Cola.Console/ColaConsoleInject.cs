using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cola.Console
{
    public static class ColaConsoleInject
    {
        public static IServiceCollection AddSingletonColaConsole(
        this IServiceCollection services,
        IConfiguration config)
        {
            services.AddSingleton<IColaConsole>(provider => new ColaConsole());
            System.Console.WriteLine("注入类型【 IColaConsole, ColaConsole 】");
            return services;
        }
    }
}
