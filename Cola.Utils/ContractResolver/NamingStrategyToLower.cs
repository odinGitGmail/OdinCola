using Newtonsoft.Json.Serialization;

namespace Cola.Utils.ContractResolver;

public class NamingStrategyToLower : NamingStrategy
{
    protected override string ResolvePropertyName(string name)
    {
        return name.ToLower();
    }
}