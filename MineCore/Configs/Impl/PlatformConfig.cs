using MineCore.Console.Logging;
using MineCore.Console.Logging.Impl;

namespace MineCore.Configs.Impl
{
    public class PlatformConfig : Config, IPlatformConfig
    {
        public override string FileName => "config.json";

        public ILoggerConfig LoggerConfig { get; set; } = new MineCoreLoggerConfig();
    }
}