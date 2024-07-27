using System.Collections.Generic;

namespace Cola.Models.Core.Models.ColaWebApi;

public class WebApiOption
{
    public List<ClientConfig> ClientConfigs { get; set; } = null!;
}