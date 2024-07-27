namespace Cola.EF.Tenant;

public class NoTenantResolutionStrategy : ITenantResolutionStrategy
{
    public string? GetTenantResolutionKey() => "1";

    public string ResolveTenantKey() => "1";
}