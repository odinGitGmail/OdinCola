using Cola.Models.Core.Models.ColaEf;
using Cola.Utils.Constants;
using Cola.Utils.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Cola.EF.Tenant;

public class DomainTenantResolutionStrategy(
    IHttpContextAccessor httpContextAccessor,
    IConfiguration configuration,
    IServiceProvider services)
    : ITenantResolutionStrategy
{
    private readonly IServiceProvider _services = services;

    public string GetTenantResolutionKey()
    {
        return httpContextAccessor.HttpContext.Request.Host.Value;
    }

    public string ResolveTenantKey()
    {
        var domain = GetTenantResolutionKey();
        var colaOrmConfig = configuration.GetSection(SystemConstant.CONSTANT_COLAORM_SECTION).Get<List<ColaEfConfig>>();
        var config = colaOrmConfig!.SingleOrDefault(d => d.Domain != null && d.Domain.StringCompareIgnoreCase(domain));
        if (config == null) throw new ArgumentException($"{domain} 无法找到对应的 tenant租户id");
        if (!int.TryParse(config.ConfigId, out var tenantId))
            throw new ArgumentException($"{domain} 中的 tenant租户id {tenantId} 无法转为int类型");
        return tenantId.ToString();
    }
}