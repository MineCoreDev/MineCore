using MineCore.Console.Logging;

namespace MineCore.Configs
{
    public interface IPlatformConfig : IConfig
    {
        ILoggerConfig LoggerConfig { get; set; }
    }
}