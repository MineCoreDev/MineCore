using MineCore.Console.Logging;

namespace MineCore.Config
{
    public interface IPlatformConfig : IConfig
    {
        ILoggerConfig LoggerConfig { get; set; }
    }
}