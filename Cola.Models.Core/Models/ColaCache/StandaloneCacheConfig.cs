namespace Cola.Models.Core.Models.ColaCache;

/// <summary>
///     单机模式
/// </summary>
public class StandaloneCacheConfig : RedisCacheConfig
{
    public string? ConnectionStrings { get; set; }
}