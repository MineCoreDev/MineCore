using MineCore.Console.Logging;
using NLog;

namespace MineCore.Impl.Console.Logging
{
    public class MineCoreLoggerConfig : ILoggerConfig
    {
        public LogLevel MinLogLevel { get; set; } = LogLevel.Info;
        public LogLevel MaxLogLevel { get; set; } = LogLevel.Fatal;
        public bool IsGenerateLogFiles { get; set; } = true;
        public bool IsCompressionOldLogFile { get; set; } = false;
    }
}