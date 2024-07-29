using Cola.Console;
using Cola.Log.Core;

namespace Cola.Exception;

public class ColaException : IColaException
{
    private readonly IColaLogs? _colaLog;
    private readonly IColaConsole? _colaConsole;

    public ColaException(IColaLogs log, IColaConsole colaConsole)
    {
        _colaLog = log;
        _colaConsole = colaConsole;
    }

    /// <summary>
    /// throw number>0
    /// </summary>
    /// <param name="i"></param>
    /// <param name="errorMessage"></param>
    /// <returns></returns>
    public System.Exception? ThrowGreaterThanZero(int i, string errorMessage)
    {
        if (i > 0)
            return ThrowException(errorMessage);
        return null;
    }
    
    /// <summary>
    /// object is null
    /// </summary>
    /// <param name="obj"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public System.Exception? ThrowIfNull<T>(T obj)
    {
        if (obj == null)
            return ThrowException($"{typeof(T).FullName} 参数不能为空");
        return null;
    }

    /// <summary>
    /// throw String Is NullOrEmpty
    /// </summary>
    /// <param name="str"></param>
    /// <param name="exMessage"></param>
    /// <returns></returns>
    public System.Exception? ThrowStringIsNullOrEmpty(string str, string exMessage)
    {
        if (string.IsNullOrEmpty(str))
            return ThrowException($"{exMessage} 不能为空");
        return null;
    }

    /// <summary>
    /// Throw Exception
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public System.Exception ThrowException(string str)
    {
        var ex = new System.Exception(str);
        if (_colaLog != null)
            _colaLog!.Error(ex);
        else
            _colaConsole!.WriteException(ex);
        return ex;
    }

    /// <summary>
    /// Throw Exception
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public System.Exception ThrowException(System.Exception ex)
    {
        if (_colaLog != null)
            _colaLog!.Error(ex);
        else
            _colaConsole!.WriteException(ex);
        return ex;
    }
}
