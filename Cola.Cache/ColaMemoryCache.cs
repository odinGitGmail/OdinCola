using System;
using System.Threading;
using Cola.Cache.IColaCache;
using Cola.Models.Core.Models.ColaCache;
using Cola.Models.Core.Models.ColaCache.Enums;
using Cola.Utils.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace Cola.Cache;

public class ColaMemoryCache : IColaMemoryCache, IDisposable
{
    public readonly IMemoryCache? MemoryCache;
    private readonly ReaderWriterLockSlim? _memoryLock;
    private readonly EnumCacheType? _memoryCacheType;
    public ColaMemoryCache(CacheConfigOption options)
    {
        if (options.CacheType == CacheType.InMemory.ToInt() || options.CacheType == CacheType.Hybrid.ToInt())
        {
            var memoryCacheOptions = new MemoryCacheOptions
            {
                //SizeLimit：设置缓存项的最大数量限制。当缓存项的数量超过这个值时，会使用 LRU 算法移除最近最少使用的缓存项。默认值为 null，表示没有限制
                SizeLimit = options.MemoryCache!.SizeLimit,
                //设置扫描过期项的频率。默认值为 30 秒，可以根据需要调整。
                ExpirationScanFrequency = TimeSpan.FromMinutes(options.MemoryCache.ExpirationScanFrequency),
                //设置缓存项数量达到最大限制后，移除缓存项的百分比。默认值为 0.1，表示移除最近最少使用的 10% 的缓存项。
                CompactionPercentage = options.MemoryCache.CompactionPercentage
            };
            MemoryCache = new MemoryCache(memoryCacheOptions);
            _memoryLock = new ReaderWriterLockSlim();
            _memoryCacheType = options.MemoryCache.CacheType.ToEnum<EnumCacheType>();
        }
    }

    public T? Get<T>(string key, int database = 0)
    {
        return MemoryCache!.Get<T>(key);
    }

    public bool Set<T>(string key, T value, TimeSpan? expiry = null, int database = 0)
    {
        if (expiry == null)
        {
            MemoryCache!.Set<T>(key, value);
        }
        else
        {
            switch (_memoryCacheType)
            {
                case EnumCacheType.absolute:
                    MemoryCache!.Set<T>(key, value, expiry.Value);
                    break;
                case EnumCacheType.relative:
                    MemoryCache!.Set<T>(key, value, new DateTimeOffset(DateTime.MinValue + expiry.Value));
                    break;
            }
        }
        return true;
    }

    public void Refresh(string key, TimeSpan expiry)
    {
        MemoryCache!.Set(key, MemoryCache!.Get(key), expiry);
    }

    public void Remove(string key)
    {
        MemoryCache!.Remove(key);
    }

    public ReaderWriterLockSlim MemoryLock()
    {
        return _memoryLock!;
    }

    public void MemoryLockMethodAsync<T>(Action<IMemoryCache> success, Action<IMemoryCache, System.Exception> fail)
    {
        _memoryLock!.EnterWriteLock();
        try
        {
            success(MemoryCache!);
        }
        catch (System.Exception ex)
        {
            fail(MemoryCache!, ex);
        }
        finally
        {
            _memoryLock.ExitWriteLock();
        }
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}