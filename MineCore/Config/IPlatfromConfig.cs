using MineCore.Console.Logging;

namespace MineCore.Config
{
    public interface IPlatfromConfig : IConfig
    {
        ILoggerConfig LoggerConfig { get; set; }
    }
}