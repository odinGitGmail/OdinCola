using Newtonsoft.Json.Serialization;

namespace Cola.Utils.ContractResolver;

public class LowerCaseContractResolver : DefaultContractResolver
{
    protected override string ResolvePropertyName(string propertyName)
    {
        return propertyName.ToLower();
    }
}