using Newtonsoft.Json.Serialization;

namespace Cola.Utils.ContractResolver;

public class ToLowerPropertyNamesContractResolver : DefaultContractResolver
{
    public ToLowerPropertyNamesContractResolver()
    {
        NamingStrategy = new NamingStrategyToLower();
    }
}