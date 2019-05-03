using NLog;

namespace MineCore.Console.Logging.Impl
{
    public class MineCoreLoggerConfig : ILoggerConfig
    {
        public LogLevel MinLogLevel { get; set; } = LogLevel.Info;
        public LogLevel MaxLogLevel { get; set; } = LogLevel.Fatal;
        public bool IsGenerateLogFiles { get; set; } = true;
        public bool IsCompressionOldLogFile { get; set; } = false;
    }
}