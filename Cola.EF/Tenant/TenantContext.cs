using System.Collections.Concurrent;
using Cola.Cache.IColaCache;
using Cola.Console;
using Cola.EF.EntityBase;
using Cola.EF.Models;
using Cola.Exception;
using Cola.Models.Core.Models.ColaCache;
using Cola.Models.Core.Models.ColaEf;
using Cola.Utils.Constants;
using Cola.Utils.Extensions;
using Cola.Utils.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SqlSugar;

namespace Cola.EF.Tenant;

public class TenantContext : ITenantContext
{
    private readonly ITenantResolutionStrategy _tenantResolutionStrategy;
    private readonly IColaCacheBase? _colaCache;
    private static readonly ConcurrentDictionary<int, SqlSugarClient> DbClients = new();
    private readonly IColaException _colaException;
    private readonly IColaConsole _colaConsole;
    private readonly ColaEfConfigOption? _efConfig;

    private TenantContext(ITenantResolutionStrategy tenantResolutionStrategy,
        IConfiguration configuration,
        IColaCacheBase? colaCache,
        IColaException colaException,
        IColaConsole colaConsole,
        List<AopOnLogExecutingModel>? aopOnLogExecutingModels,
        List<AopOnErrorModel>? aopOnErrorModels,
        List<GlobalQueryFilter>? globalQueryFilters)
    {
        _tenantResolutionStrategy = tenantResolutionStrategy;
        _efConfig = configuration.GetSection(SystemConstant.CONSTANT_COLAORM_SECTION).Get<ColaEfConfigOption>();
        _colaCache = colaCache;
        _colaException = colaException;
        _colaConsole = colaConsole;
        SetDbClientByTenant(aopOnLogExecutingModels, aopOnErrorModels, globalQueryFilters);
    }

    public static TenantContext Create(IServiceProvider serviceProvider, IConfiguration configuration,List<AopOnLogExecutingModel>? aopOnLogExecutingModels, List<AopOnErrorModel>? aopOnErrorModels, List<GlobalQueryFilter>? globalQueryFilters)
    {
        var exceptionHelper = serviceProvider.GetService<IColaException>();
        if (exceptionHelper == null) throw new System.Exception("未注入 IColaException 类型");
        var colaConsole = serviceProvider.GetService<IColaConsole>();
        if (exceptionHelper == null) throw new System.Exception("未注入 IColaConsole 类型");
        if (configuration == null) throw new System.Exception("未注入 IConfiguration 类型");
        
        var tenantResolutionStrategy = serviceProvider.GetService<ITenantResolutionStrategy>();
        if (tenantResolutionStrategy == null) throw new System.Exception("未注入 ITenantResolutionStrategy 类型");
        
        var cacheConfig = configuration.GetSection(SystemConstant.CONSTANT_COLACACHE_SECTION).Get<CacheConfigOption>();
        IColaCacheBase? colaCache = null;
        if (cacheConfig!.CacheType == CacheType.Hybrid.ToInt())
        {
            var colaHybridCache = serviceProvider.GetService<IColaHybridCache>();
            colaCache = colaHybridCache ?? throw new System.Exception("未注入 IColaHybridCache 类型");
        }
        else if (cacheConfig.CacheType == CacheType.Redis.ToInt())
        {
            var colaRedisCache = serviceProvider.GetService<IColaRedisCache>();
            colaCache  = colaRedisCache ?? throw new System.Exception("未注入 IColaRedisCache 类型");
        }
        else if (cacheConfig.CacheType == CacheType.InMemory.ToInt())
        {
            var colaMemoryCache = serviceProvider.GetService<IColaMemoryCache>();
            colaCache  = colaMemoryCache ?? throw new System.Exception("未注入 IColaMemoryCache 类型");
        }
        return new TenantContext(tenantResolutionStrategy, configuration, colaCache, exceptionHelper, colaConsole!, aopOnLogExecutingModels, aopOnErrorModels, globalQueryFilters);
    }

    public int? TenantId
    {
        get
        {
            var tenantKey = _tenantResolutionStrategy.GetTenantResolutionKey();
            if (_colaCache == null) return _tenantResolutionStrategy.ResolveTenantKey().ToInt();
            string? tenantId = _colaCache.Get<string>(tenantKey!);
            if (tenantId != null) return tenantId.ToInt();
            tenantId = _tenantResolutionStrategy.ResolveTenantKey();
            _colaCache.Set(tenantKey!, tenantId, new TimeSpan(UnixTimeHelper.FromDateTime(DateTime.Now.AddYears(1))));
            return tenantId.ToInt();
        }
    }

    public void SetDbClientByTenant(List<AopOnLogExecutingModel>? aopOnLogExecutingModels,List<AopOnErrorModel>? aopOnErrorModels, List<GlobalQueryFilter>? globalQueryFilters)
    {
        foreach (var config in _efConfig!.ColaOrmConfig!)
        {
            var connectionConfig = new ConnectionConfig
            {
                ConfigId = config.ConfigId,
                // 配置连接字符串
                ConnectionString = config.ConnectionString,
                DbType = config.DbType!.ConvertStringToEnum<DbType>(),
                IsAutoCloseConnection = config.IsAutoCloseConnection,
                InitKeyType = InitKeyType.Attribute
            };
            var client = new SqlSugarClient(connectionConfig);

            #region OnLogExecuting

            if (config.EnableLogAop)
            {
                if (aopOnLogExecutingModels != null)
                {
                    var aopOnLogExecutingModel = aopOnLogExecutingModels.SingleOrDefault(m => m.ConfigId == config.ConfigId);
                    if (aopOnLogExecutingModel != null)
                        client.Aop.OnLogExecuting = aopOnLogExecutingModel.AopOnLogExecuting;
                }
                else
                {
                    client.Aop.OnLogExecuting = (sql, parameters) =>
                    {
                        _colaConsole!.WriteInfo($"sql:\n\t{sql}");
                        _colaConsole!.WriteInfo("parameters is :");
                        foreach (var parameter in parameters)
                        {
                            System.Console.WriteLine($"\tparameter name:{parameter.ParameterName}\tparameter value:{parameter.Value!}");
                        }
                    };
                }
            }

            #endregion
            
            #region OnError

            if (config.EnableErrorAop)
            {
                if (aopOnErrorModels != null)
                {
                    var aopOnErrorModel = aopOnErrorModels.SingleOrDefault(m => m.ConfigId == config.ConfigId);
                    if (aopOnErrorModel != null)
                        client.Aop.OnError = aopOnErrorModel.AopOnError;
                }
                else
                {
                    client.Aop.OnError = exception =>
                    {
                        _colaConsole!.WriteException($"Sql Error:");
                        _colaConsole!.WriteException(JsonConvert.SerializeObject(exception).ToJsonFormatString());
                    };
                }
            }

            #endregion

            #region GlobalFilter

            if (config.EnableGlobalFilter)
            {
                if (globalQueryFilters != null)
                {
                    var globalQueryFilter = globalQueryFilters.SingleOrDefault(m => m.ConfigId == config.ConfigId);
                    if (globalQueryFilter != null)
                        globalQueryFilter.QueryFilter!(client.QueryFilter);
                }
                else
                {
                    client.QueryFilter.AddTableFilter<IDeleted>(t => t.IsDelete == false);
                }
            }
            
            #endregion
            
            DbClients.TryAdd(config.ConfigId!.ToInt(), client);
        }
    }
    
    public SqlSugarClient GetDbClientByTenant(int? tenantId)
    {
        if(tenantId==null) throw _colaException.ThrowException($"{tenantId} 无法找到对应的tenantKey");
        if (DbClients.TryGetValue(tenantId.Value, out SqlSugarClient? sqlSugarClient))
        {
            return sqlSugarClient;
        }
        throw _colaException.ThrowException($"{tenantId} 无法找到对应的链接对象");
    }
}
