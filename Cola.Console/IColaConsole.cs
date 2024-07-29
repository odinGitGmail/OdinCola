using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cola.Console
{
    public interface IColaConsole
    {
        /// <summary>
        ///  BackgroundColor:Green  ForegroundColor: White
        /// </summary> 
        /// <param name="str"></param>
        void WriteInfo(Object str);

        void WriteInfo(int str);

        void WriteInfo(string str);

        void WriteInfo(string format, params object?[]? arg);

        /// <summary>
        /// BackgroundColor:DarkRed  ForegroundColor: White
        /// </summary>
        /// <param name="ex"></param>
        void WriteException(Exception ex);

        void WriteException(string format);

        void WriteException(string format, params object?[]? arg);

        /// <summary>
        /// foregroundColor:White   backgroundColor:Black
        /// </summary>
        /// <param name="format"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="backgroundColor"></param>
        /// <param name="arg"></param>
        void WriteLine(string format, ConsoleColor foregroundColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black, params object?[]? arg);

        void WriteLine(string format, ConsoleColor foregroundColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black);
    }
}
