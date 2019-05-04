using System.Net;
using System.Threading;
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
        public IServerNetworkManager NetworkManager { get; private set; }

        protected override bool Init()
        {
            bool result = base.Init();

            PlatformLogger.Info(StringManager.GetString("minecore.app.start"));
            NetworkManager = new ServerNetworkManager(new IPEndPoint(IPAddress.Any, 19132),
                new ServerListData(new MineCraftProtocol()));

            return result;
        }

        public override PlatformStartResult Start()
        {
            PlatformStartResult result = base.Start();
            if (result == PlatformStartResult.Failed)
                return PlatformStartResult.Failed;

            NetworkManager.Start();

            IPEndPoint endPoint = NetworkManager.ServerEndPoint;
            PlatformLogger.Info(StringManager.GetString("minecore.network.start", endPoint.Address.ToString(),
                endPoint.Port));

            return PlatformStartResult.Success;
        }
    }
}