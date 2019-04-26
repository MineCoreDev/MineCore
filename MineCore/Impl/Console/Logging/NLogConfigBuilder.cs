using MineCore.Console.Logging;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace MineCore.Impl.Console.Logging
{
    public class NLogConfigBuilder : INLogConfigBuilder
    {
        public LoggingConfiguration GetConfiguration(ILoggerConfig config)
        {
            LoggingConfiguration conf = new LoggingConfiguration();
            conf.AddTarget("console", new ColoredConsoleTarget()
            {
                UseDefaultRowHighlightingRules = false,
                Layout = new SimpleLayout("[${longdate}] [${threadname} /${uppercase:${level:padding=5}}] ${message}")
            });
            conf.AddRule(config.MinLogLevel, config.MaxLogLevel, "console");

            return conf;
        }
    }
}