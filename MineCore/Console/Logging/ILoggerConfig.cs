using NLog;

namespace MineCore.Console.Logging
{
    public interface ILoggerConfig
    {
        LogLevels MinLogLevel { get; set; }
        LogLevels MaxLogLevel { get; set; }

        bool IsGenerateLogFiles { get; set; }
        bool IsCompressionOldLogFile { get; set; }
    }
}