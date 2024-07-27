using Cola.Utils.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Cola.WebApi;

public class OdinBadRequest : BadRequestObjectResult
{
    public OdinBadRequest(string errorCode, string message) : base(new ErrorModel(errorCode, message))
    {
        StatusCode = errorCode.ToInt();
    }
}