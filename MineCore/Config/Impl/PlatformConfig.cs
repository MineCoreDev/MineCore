using System;
using System.IO;
using System.Text;
using MineCore.Console.Logging;
using MineCore.Console.Logging.Impl;
using YamlDotNet.Serialization;

namespace MineCore.Config.Impl
{
    public class PlatformConfig : Config, IPlatformConfig
    {
        public override string FileName => "config.json";

        public ILoggerConfig LoggerConfig { get; set; } = new MineCoreLoggerConfig();
    }
}