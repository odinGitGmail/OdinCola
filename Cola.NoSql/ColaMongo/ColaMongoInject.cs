using Cola.Console;
using Cola.Models.Core.Models.ColaMongo;
using Cola.Utils.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cola.NoSql.ColaMongo;

public static class ColaMongoInject
{
    /// <summary>
    ///     inject SnowFlake
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    /// <param name="config">config</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddSingletonColaMongo(
        this IServiceCollection services, 
        IConfiguration config)
    {
        var mongoDbConfig = config.GetSection(SystemConstant.CONSTANT_COLANOSQL_MONGO_SECTION).Get<MongoDbConfig>();
        services.AddSingleton<IColaMongo>(new ColaMongo(mongoDbConfig!));
        ColaConsole.WriteInfo("注入类型【 IColaMongo, ColaMongo 】");
        return services;
    }
    
    /// <summary>
    ///     inject SnowFlake
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
        ColaConsole.WriteInfo("注入类型【 IColaMongo, ColaMongo 】");
        return services;
    }
}