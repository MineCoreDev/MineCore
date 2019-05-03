using System.Threading;
using MineCore.Console;
using MineCore.Console.Impl;
using MineCore.Languages;
using MineCore.Platforms;
using MineCore.Services.Impl;
using MineCore.Utils;
using NLog;

namespace MineCore.Server
{
    public class ServerPlatform : ServiceContainer, IServerPlatform
    {
        public Logger PlatformLogger { get; } = LogManager.GetCurrentClassLogger();

        public IConsole Console { get; private set; }

        public ServiceContainer ServiceContainer { get; private set; }

        public PlatformStartResult Start()
        {
            StringManager.Init();
            Console = new MineCoreConsole();

            Thread.CurrentThread.Name = StringManager.GetString("minecore.thread.server");

            PlatformLogger.Info(StringManager.GetString("minecore.app.start"));

            PlatformLogger.Info(StringManager.GetString("minecore.service_container.start"));
            ServiceContainer = new ServiceContainer();
            ServiceContainer.LoadPlatforms = typeof(ServerPlatformServiceAttribute);
            ServiceContainer.LoadServices();
            PlatformLogger.Info(StringManager.GetString("minecore.service_container.end"));

            Console.Start();


            while (true)
            {
                Thread.Sleep(1);
            }

            return PlatformStartResult.Success;
        }
    }
}