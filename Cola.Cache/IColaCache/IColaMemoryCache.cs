using System;
using System.Threading;
using Microsoft.Extensions.Caching.Memory;

namespace Cola.Cache.IColaCache;

public interface IColaMemoryCache : IColaCacheBase
{
    void Refresh(string key, TimeSpan expiry);
    void Remove(string key);
    ReaderWriterLockSlim MemoryLock();
    void MemoryLockMethodAsync<T>(Action<IMemoryCache> success, Action<IMemoryCache, System.Exception> fail);
}