using SqlSugar;

namespace Cola.EF.Models;

public class GlobalQueryFilter
{
    public string? ConfigId { get; set; }
    public Action<QueryFilterProvider>? QueryFilter { get; set; }
}