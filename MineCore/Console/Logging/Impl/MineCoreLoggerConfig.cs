using NLog;

namespace MineCore.Console.Logging.Impl
{
    public class MineCoreLoggerConfig : ILoggerConfig
    {
        public LogLevels MinLogLevel { get; set; } = LogLevels.Info;
        public LogLevels MaxLogLevel { get; set; } = LogLevels.Fatal;
        public bool IsGenerateLogFiles { get; set; } = true;
        public bool IsCompressionOldLogFile { get; set; } = false;
    }
}