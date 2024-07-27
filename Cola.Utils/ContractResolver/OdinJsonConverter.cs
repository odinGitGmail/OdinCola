using Newtonsoft.Json.Serialization;

namespace Cola.Utils.ContractResolver;

public class OdinJsonConverter
{
    public static IContractResolver SetOdinJsonConverter(EnumOdinJsonConverter enumCase)
    {
        return enumCase switch
        {
            EnumOdinJsonConverter.CamelCase => new CamelCasePropertyNamesContractResolver(),
            EnumOdinJsonConverter.LowerCase => new ToLowerPropertyNamesContractResolver(),
            _ => new DefaultContractResolver()
        };
    }
}