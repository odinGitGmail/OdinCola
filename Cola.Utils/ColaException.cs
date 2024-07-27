using Cola.Utils.Enums;
using Cola.Utils.Extensions;

namespace Cola.Utils;

public class ColaException : Exception
{
    public ColaException(EnumException enumException) : base(enumException.GetDescription())
    {
    }

    public ColaException(EnumException enumException, string msg) : base(string.Format(enumException.GetDescription(),
        msg))
    {
    }

    public ColaException(string errorMessage) : base(errorMessage)
    {
    }
}