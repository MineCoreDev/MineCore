using System.Threading;
using MineCore.Impl.Console;
using MineCore.Impl.Console.Logging;
using MineCore.Impl.Services;
using MineCore.Languages;
using MineCore.Platforms;
using MineCore.Utils;
using NLog;

namespace MineCore.Server
{
    public class ServerPlatform : ServiceContainer, IServerPlatform
    {
        public Logger PlatformLogger { get; } = LogManager.GetCurrentClassLogger();
        public MineCoreLoggerConfig LoggerConfig { get; } = new MineCoreLoggerConfig();

        public ServiceContainer ServiceContainer { get; private set; }

        public MineCoreConsole Console { get; private set; }

        public PlatformStartResult Start()
        {
            StringManager.Init();
            LogManager.Configuration = new NLogConfigBuilder().GetConfiguration(LoggerConfig);

            Thread.CurrentThread.Name = StringManager.GetString("minecore.thread.server");

            PlatformLogger.Info(StringManager.GetString("minecore.app.start"));

            Console = new MineCoreConsole();
            Console.StartWorker();

            PlatformLogger.Info(StringManager.GetString("minecore.service_container.start"));

            ServiceContainer = new ServiceContainer();
            ServiceContainer.LoadServices();

            while (true)
            {
                Thread.Sleep(1);
            }

            return PlatformStartResult.Success;
        }
    }
}