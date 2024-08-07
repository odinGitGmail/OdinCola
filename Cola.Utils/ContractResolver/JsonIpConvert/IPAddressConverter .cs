using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cola.Utils.ContractResolver.JsonIpConvert;

public class IpAddressConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(IPAddress);
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        var ip = (IPAddress)value!;
        writer.WriteValue(ip?.ToString());
    }

    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
        JsonSerializer serializer)
    {
        var token = JToken.Load(reader);
        return IPAddress.Parse(token.Value<string>() ?? string.Empty);
    }
}