using System.Net;
using System.Threading;
using MineCore.Configs;
using MineCore.Configs.Impl;
using MineCore.Console;
using MineCore.Console.Impl;
using MineCore.Languages;
using MineCore.Net;
using MineCore.Net.Impl;
using MineCore.Platforms;
using MineCore.Platforms.Impl;
using MineCore.Utils;
using NLog;

namespace MineCore.Server
{
    public class ServerPlatform : Platform, IServerPlatform
    {
        public IServerPlatformConfig ServerConfig { get; private set; }
        public IMineCraftProtocol Protocol { get; private set; }
        public IServerNetworkManager NetworkManager { get; private set; }

        protected override bool Init()
        {
            bool result = base.Init();

            (ConfigLoadResult result, ServerPlatformConfig config) configData =
                Configs.Impl.Config.Load<ServerPlatformConfig>("server_config.json");
            if (configData.result == ConfigLoadResult.Success || configData.result == ConfigLoadResult.Upgrade)
                ServerConfig = configData.config;
            else
                return false;

            PlatformLogger.Info(StringManager.GetString("minecore.app.start"));
            Protocol = new MineCraftProtocol();
            NetworkManager = new ServerNetworkManager(new IPEndPoint(IPAddress.Any, ServerConfig.ServerPort), this);

            return result;
        }

        public override PlatformStartResult Start()
        {
            PlatformStartResult result = base.Start();
            if (result == PlatformStartResult.Failed)
                return PlatformStartResult.Failed;
            else if (result == PlatformStartResult.Started)
                return PlatformStartResult.Started;

            NetworkManager.Start();

            IPEndPoint endPoint = NetworkManager.ServerEndPoint;
            PlatformLogger.Info(StringManager.GetString("minecore.network.start", endPoint.Address.ToString(),
                endPoint.Port));

            return PlatformStartResult.Success;
        }

        public override PlatformStopResult Stop()
        {
            PlatformStopResult result = base.Stop();
            if (result == PlatformStopResult.Failed)
                return PlatformStopResult.Failed;
            else if (result == PlatformStopResult.Stopped)
                return PlatformStopResult.Stopped;

            NetworkManager.Dispose();

            return PlatformStopResult.Success;
        }
    }
}