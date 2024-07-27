using Cola.Models.Core.Enums.Logs;
using Cola.Models.Core.Models.ColaLog;
using Microsoft.Extensions.DependencyInjection;

namespace Cola.Log.Core.LogUtils;

public class LogInfoUtil : AbsLogContent
{
    public LogInfoUtil(EnumLogLevel logLevel, LogConfig config, IServiceCollection? service) : base(logLevel, config,
        service)
    {
    }

    public override LogResponse? WriteLog(LogInfo log)
    {
        System.Console.ForegroundColor = ConsoleColor.Green;
        var logResult = WriteLogContent(log);
        System.Console.ResetColor();
        return logResult;
    }
}