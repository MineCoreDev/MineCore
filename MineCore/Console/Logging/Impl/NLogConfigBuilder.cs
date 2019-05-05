using MineCore.Utils;
using NLog;
using NLog.Config;
using NLog.Layouts;

namespace MineCore.Console.Logging.Impl
{
    public class NLogConfigBuilder : INLogConfigBuilder
    {
        public MineCoreConsoleTarget ConsoleTarget { get; private set; }

        public LoggingConfiguration GetConfiguration(ILoggerConfig config)
        {
            config.ThrownOnArgNull(nameof(config));

            LoggingConfiguration conf = new LoggingConfiguration();
            ConsoleTarget = new MineCoreConsoleTarget()
            {
                Layout = new SimpleLayout("[${longdate}] [${threadname} /${uppercase:${level:padding=5}}] ${message}")
            };

            conf.AddTarget("console", ConsoleTarget);
            conf.AddRule(LogLevel.FromString(config.MinLogLevel.ToString()),
                LogLevel.FromString(config.MaxLogLevel.ToString()), "console");

            return conf;
        }
    }
}