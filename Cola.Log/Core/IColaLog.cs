using Cola.Models.Core.Enums.Logs;
using Cola.Models.Core.Models.ColaLog;

namespace Cola.Log.Core;

public interface IColaLog
{
    LogModel GenerateLog(EnumLogLevel logLevel, LogInfo log);
}