using System.Collections.Generic;
using MineCore.Console.Logging;
using MineCore.Utils;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace MineCore.Impl.Console.Logging
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
            conf.AddRule(config.MinLogLevel, config.MaxLogLevel, "console");

            return conf;
        }
    }
}