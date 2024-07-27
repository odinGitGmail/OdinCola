using System;
using System.Data;
using Cola.Utils.Extensions;

namespace Cola.Models.Core.Models.ColaEf;

public class ColaEfConfig
{
    public string? ConfigId { get; set; } = "1";
    public string? Domain { get; set; }
    public string? DbType { get; set; }

    public string? ConnectionString { get; set; }

    public bool IsAutoCloseConnection { get; set; } = true;

    public bool EnableLogAop { get; set; } = true;

    public bool EnableErrorAop { get; set; } = true;

    public bool EnableGlobalFilter { get; set; }

    public SqlSugar.DbType GetSqlSugarDbType()
    {
        if (string.IsNullOrEmpty(DbType)) throw new Exception("SqlSugar配置没有明确指定DbType");
        return DbType!.ConvertStringToEnum<SqlSugar.DbType>();
    }
}