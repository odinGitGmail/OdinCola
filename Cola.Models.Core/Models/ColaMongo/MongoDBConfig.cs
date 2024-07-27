namespace Cola.Models.Core.Models.ColaMongo;

/// <summary>
/// MongoDbConfig.
/// </summary>
public class MongoDbConfig
{
    /// <summary>
    /// 连接字符串.
    /// </summary>
    public string? ConnName { get; set; }

    /// <summary>
    /// 数据库名称.
    /// </summary>
    public string? DatabaseName { get; set; }
}