using Cola.Models.Core.Enums.Logs;
using Cola.Models.Core.Models.ColaLog;
using Microsoft.Extensions.DependencyInjection;

namespace Cola.Log.Core.LogUtils;

public class LogErrorUtil : AbsLogException
{
    public LogErrorUtil(EnumLogLevel logLevel, LogConfig config, IServiceCollection? service) : base(logLevel, config,
        service)
    {
    }

    public override LogResponse? WriteLog(LogInfo log)
    {
        System.Console.ForegroundColor = ConsoleColor.Red;
        var logResult = WriteLogContent(log);
        System.Console.ResetColor();
        return logResult;
    }
}