using NLog.Config;

namespace MineCore.Console.Logging
{
    public interface INLogConfigBuilder
    {
        LoggingConfiguration GetConfiguration(ILoggerConfig config);
    }
}