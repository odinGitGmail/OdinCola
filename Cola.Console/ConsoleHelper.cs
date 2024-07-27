using Newtonsoft.Json;

namespace Cola.Console;

public class ColaConsole
{
    private static string TimestampForma = "[yyyy-MM-dd HH:mm:ss] ";
    
    /// <summary>
    ///  BackgroundColor:Green  ForegroundColor: White
    /// </summary> 
    /// <param name="str"></param>
    public static void WriteInfo(Object str)
    {
        var dt = DateTime.Now.ToString(TimestampForma);
        System.Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.WriteLine(dt + str);
        System.Console.ResetColor();
    }
    
    public static void WriteInfo(int str)
    {
        var dt = DateTime.Now.ToString(TimestampForma);
        System.Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.WriteLine(dt + str);
        System.Console.ResetColor();
    }
    
    public static void WriteInfo(string str)
    {
        var dt = DateTime.Now.ToString(TimestampForma);
        System.Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.WriteLine(dt + str);
        System.Console.ResetColor();
    }

    public static void WriteInfo(string format, params object?[]? arg)
    {
        var dt = DateTime.Now.ToString(TimestampForma);
        System.Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.WriteLine(dt + format, arg);
        System.Console.ResetColor();
    }

    /// <summary>
    /// BackgroundColor:DarkRed  ForegroundColor: White
    /// </summary>
    /// <param name="ex"></param>
    public static void WriteException(Exception ex)
    {
        var dt = DateTime.Now.ToString(TimestampForma);
        System.Console.BackgroundColor = ConsoleColor.DarkRed;
        System.Console.ForegroundColor = ConsoleColor.White;
        System.Console.WriteLine(dt + JsonConvert.SerializeObject(ex));
        System.Console.ResetColor();
    }

    public static void WriteException(string format)
    {
        var dt = DateTime.Now.ToString(TimestampForma);
        System.Console.BackgroundColor = ConsoleColor.DarkRed;
        System.Console.ForegroundColor = ConsoleColor.White;
        System.Console.WriteLine(dt + format);
        System.Console.ResetColor();
    }

    public static void WriteException(string format, params object?[]? arg)
    {
        var dt = DateTime.Now.ToString(TimestampForma);
        System.Console.BackgroundColor = ConsoleColor.DarkRed;
        System.Console.ForegroundColor = ConsoleColor.White;
        System.Console.WriteLine(dt + format, arg);
        System.Console.ResetColor();
    }

    /// <summary>
    /// foregroundColor:White   backgroundColor:Black
    /// </summary>
    /// <param name="format"></param>
    /// <param name="foregroundColor"></param>
    /// <param name="backgroundColor"></param>
    /// <param name="arg"></param>
    public static void WriteLine(string format, ConsoleColor foregroundColor = ConsoleColor.White,
        ConsoleColor backgroundColor = ConsoleColor.Black, params object?[]? arg)
    {
        var dt = DateTime.Now.ToString(TimestampForma);
        System.Console.ForegroundColor = foregroundColor;
        System.Console.BackgroundColor = backgroundColor;
        System.Console.WriteLine(dt + format, arg);
        System.Console.ResetColor();
    }

    public static void WriteLine(string format, ConsoleColor foregroundColor = ConsoleColor.White,
        ConsoleColor backgroundColor = ConsoleColor.Black)
    {
        var dt = DateTime.Now.ToString(TimestampForma);
        System.Console.ForegroundColor = foregroundColor;
        System.Console.BackgroundColor = backgroundColor;
        System.Console.WriteLine(dt + format);
        System.Console.ResetColor();
    }
}