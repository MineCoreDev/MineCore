using NLog;

namespace MineCore.Console.Logging
{
    public interface ILoggerConfig
    {
        LogLevel MinLogLevel { get; set; }
        LogLevel MaxLogLevel { get; set; }

        bool IsGenerateLogFiles { get; set; }
        bool IsCompressionOldLogFile { get; set; }
    }
}