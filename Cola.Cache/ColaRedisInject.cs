using System;
using Cola.Cache.IColaCache;
using Cola.Console;
using Cola.Models.Core.Models.ColaCache;
using Cola.Utils.Constants;
using Cola.Utils.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cola.Cache;

public static class ColaRedisInject
{
    public static IServiceCollection AddSingletonColaCache(
        this IServiceCollection services,
        IConfiguration config)
    {
        var cacheConfig = config.GetSection(SystemConstant.CONSTANT_COLACACHE_SECTION).Get<CacheConfigOption>();
        return InjectCache(services, cacheConfig!);
    }


    public static IServiceCollection AddSingletonColaCache(
        this IServiceCollection services,
        Action<CacheConfigOption> action)
    {
        var cacheConfig = new CacheConfigOption();
        action(cacheConfig);
        return InjectCache(services, cacheConfig);
    }

    private static IServiceCollection InjectCache(IServiceCollection services, CacheConfigOption cacheConfig)
    {
        var colaConsole = services.BuildServiceProvider().GetService<IColaConsole>();
        if (cacheConfig.CacheType == CacheType.NoCache.ToInt())
        {
            return services;
        }
        if (cacheConfig.CacheType == CacheType.Hybrid.ToInt())
        {
            services.AddSingleton<IColaRedisCache>(provider => new ColaRedis(cacheConfig));
            colaConsole!.WriteInfo("注入类型【 ColaRedis, IColaRedisCache 】");
            services.AddSingleton<IColaMemoryCache>(provider => new ColaMemoryCache(cacheConfig));
            colaConsole!.WriteInfo("注入类型【 ColaMemoryCache, IColaMemoryCache 】");
            services.AddSingleton<IColaHybridCache, ColaHybridCache>();
            services.AddSingleton<IColaHybridCache>(s => ColaHybridCache.Create(s));
            colaConsole!.WriteInfo("注入类型【 ColaHybridCache, IColaHybridCache 】");
        }
        else if (cacheConfig.CacheType == CacheType.Redis.ToInt())
        {
            services.AddSingleton<IColaRedisCache>(provider => new ColaRedis(cacheConfig));
            colaConsole!.WriteInfo("注入类型【 ColaRedis, IColaRedisCache 】");
        }
        else if (cacheConfig.CacheType == CacheType.InMemory.ToInt())
        {
            services.AddSingleton<IColaMemoryCache>(provider => new ColaMemoryCache(cacheConfig));
            colaConsole!.WriteInfo("注入类型【 ColaMemoryCache, IColaMemoryCache 】");
        }
        return services;
    }
}
