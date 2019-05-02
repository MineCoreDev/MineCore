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
        public LoggingConfiguration GetConfiguration(ILoggerConfig config)
        {
            config.ThrownOnArgNull(nameof(config));

            LoggingConfiguration conf = new LoggingConfiguration();

            ColoredConsoleTarget target = new ColoredConsoleTarget()
            {
                UseDefaultRowHighlightingRules = false,
                Layout = new SimpleLayout("[${longdate}] [${threadname} /${uppercase:${level:padding=5}}] ${message}")
            };
            AddRowColorRules(target);
            AddWordColorRules(target);

            conf.AddTarget("console", target);
            conf.AddRule(config.MinLogLevel, config.MaxLogLevel, "console");

            return conf;
        }

        private void AddRowColorRules(ColoredConsoleTarget target)
        {
            target.ThrownOnArgNull(nameof(target));

            target.RowHighlightingRules.Add(new ConsoleRowHighlightingRule
            {
                Condition = "level == LogLevel.Debug",
                ForegroundColor = ConsoleOutputColor.DarkGray
            });
            target.RowHighlightingRules.Add(new ConsoleRowHighlightingRule
            {
                Condition = "level == LogLevel.Trace",
                ForegroundColor = ConsoleOutputColor.Cyan
            });
            target.RowHighlightingRules.Add(new ConsoleRowHighlightingRule
            {
                Condition = "level == LogLevel.Info",
                ForegroundColor = ConsoleOutputColor.Gray
            });
            target.RowHighlightingRules.Add(new ConsoleRowHighlightingRule
            {
                Condition = "level == LogLevel.Warn",
                ForegroundColor = ConsoleOutputColor.Yellow
            });
            target.RowHighlightingRules.Add(new ConsoleRowHighlightingRule
            {
                Condition = "level == LogLevel.Error",
                ForegroundColor = ConsoleOutputColor.Red
            });
            target.RowHighlightingRules.Add(new ConsoleRowHighlightingRule
            {
                Condition = "level == LogLevel.Fatal",
                ForegroundColor = ConsoleOutputColor.White,
                BackgroundColor = ConsoleOutputColor.Red
            });
        }

        private void AddWordColorRules(ColoredConsoleTarget target)
        {
            target.ThrownOnArgNull(nameof(target));

            target.WordHighlightingRules.Add(new ConsoleWordHighlightingRule
            {
                Regex = "§0[^§\n]+",
                ForegroundColor = ConsoleOutputColor.Black
            });
            target.WordHighlightingRules.Add(new ConsoleWordHighlightingRule
            {
                Regex = "§1[^§\n]+",
                ForegroundColor = ConsoleOutputColor.DarkBlue
            });
            target.WordHighlightingRules.Add(new ConsoleWordHighlightingRule
            {
                Regex = "§2[^§\n]+",
                ForegroundColor = ConsoleOutputColor.DarkGreen
            });
            target.WordHighlightingRules.Add(new ConsoleWordHighlightingRule
            {
                Regex = "§3[^§\n]+",
                ForegroundColor = ConsoleOutputColor.DarkCyan
            });
            target.WordHighlightingRules.Add(new ConsoleWordHighlightingRule
            {
                Regex = "§4[^§\n]+",
                ForegroundColor = ConsoleOutputColor.DarkRed
            });
            target.WordHighlightingRules.Add(new ConsoleWordHighlightingRule
            {
                Regex = "§5[^§\n]+",
                ForegroundColor = ConsoleOutputColor.DarkMagenta
            });
            target.WordHighlightingRules.Add(new ConsoleWordHighlightingRule
            {
                Regex = "§6[^§\n]+",
                ForegroundColor = ConsoleOutputColor.DarkYellow
            });
            target.WordHighlightingRules.Add(new ConsoleWordHighlightingRule
            {
                Regex = "§7[^§\n]+",
                ForegroundColor = ConsoleOutputColor.Gray
            });
            target.WordHighlightingRules.Add(new ConsoleWordHighlightingRule
            {
                Regex = "§8[^§\n]+",
                ForegroundColor = ConsoleOutputColor.DarkGray
            });
            target.WordHighlightingRules.Add(new ConsoleWordHighlightingRule
            {
                Regex = "§9[^§\n]+",
                ForegroundColor = ConsoleOutputColor.Blue
            });
            target.WordHighlightingRules.Add(new ConsoleWordHighlightingRule
            {
                Regex = "§a[^§\n]+",
                ForegroundColor = ConsoleOutputColor.Green
            });
            target.WordHighlightingRules.Add(new ConsoleWordHighlightingRule
            {
                Regex = "§b[^§\n]+",
                ForegroundColor = ConsoleOutputColor.Cyan
            });
            target.WordHighlightingRules.Add(new ConsoleWordHighlightingRule
            {
                Regex = "§c[^§\n]+",
                ForegroundColor = ConsoleOutputColor.Red
            });
            target.WordHighlightingRules.Add(new ConsoleWordHighlightingRule
            {
                Regex = "§d[^§\n]+",
                ForegroundColor = ConsoleOutputColor.Magenta
            });
            target.WordHighlightingRules.Add(new ConsoleWordHighlightingRule
            {
                Regex = "§e[^§\n]+",
                ForegroundColor = ConsoleOutputColor.Yellow
            });
            target.WordHighlightingRules.Add(new ConsoleWordHighlightingRule
            {
                Regex = "§f[^§\n]+",
                ForegroundColor = ConsoleOutputColor.White
            });
            target.WordHighlightingRules.Add(new ConsoleWordHighlightingRule
            {
                Regex = "§r[^§\n]+",
                ForegroundColor = ConsoleOutputColor.NoChange
            });
        }
    }
}