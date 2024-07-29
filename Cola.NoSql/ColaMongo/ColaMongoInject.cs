using Cola.Console;
using Cola.Models.Core.Models.ColaMongo;
using Cola.Utils.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cola.NoSql.ColaMongo;

public static class ColaMongoInject
{
    /// <summary>
    ///     inject ColaMongoInject
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    /// <param name="config">config</param>
    /// <param name="colaConsole">colaConsole</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddSingletonColaMongo(
        this IServiceCollection services,
        IConfiguration config)
    {
        var mongoDbConfig = config.GetSection(SystemConstant.CONSTANT_COLANOSQL_MONGO_SECTION).Get<MongoDbConfig>();
        services.AddSingleton<IColaMongo>(new ColaMongo(mongoDbConfig!));
        var colaConsole = services.BuildServiceProvider().GetService<IColaConsole>();
        colaConsole!.WriteInfo("注入类型【 IColaMongo, ColaMongo 】");
        return services;
    }

    /// <summary>
    ///     inject ColaMongoInject
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    /// <param name="action">action</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddSingletonColaMongo(
        this IServiceCollection services,
        Action<MongoDbConfig> action)
    {
        var opts = new MongoDbConfig();
        action(opts);
        services.AddSingleton<IColaMongo>(new ColaMongo(opts));
        var colaConsole = services.BuildServiceProvider().GetService<IColaConsole>();
        colaConsole!.WriteInfo("注入类型【 IColaMongo, ColaMongo 】");
        return services;
    }
}
