using System.Net;
using MineCore.Impl.Net;
using MineCore.Net;
using MineCore.Services.Net;
using RakDotNet.Utils;

namespace MineCore.Impl.Services.Net
{
    [ServerPlatformService]
    public class ServerNetworkService : MineCoreService, IServerNetworkService
    {
        public IMineCraftProtocol Protocol { get; private set; }
        public IServerListData ListData { get; private set; }
        public IServerNetworkManager Manager { get; private set; }

        public override void OnEnable()
        {
            Logger.PrintCallBack = log => ServiceLogger.Debug(log.Message);

            Protocol = new MineCraftProtocol();
            ListData = new ServerListData(Protocol);
            Manager = new ServerNetworkManager(new IPEndPoint(IPAddress.Any, 19132), ListData);
            Manager.Start();
        }

        public override void OnDisable()
        {
            Manager.Dispose();
        }
    }
}