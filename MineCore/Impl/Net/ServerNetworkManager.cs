using System;
using System.Net;
using MineCore.Net;
using MineCore.Utils;
using RakDotNet.Minecraft;

namespace MineCore.Impl.Net
{
    public class ServerNetworkManager : IServerNetworkManager
    {
        private MinecraftServer _server;

        public ServerNetworkManager(IPEndPoint endPoint, IServerListData listData)
        {
            endPoint.ThrownOnArgNull(nameof(endPoint));
            listData.ThrownOnArgNull(nameof(listData));

            _server = new MinecraftServer(endPoint);
            UpdateServerList(listData);
        }

        public void Start()
        {
            _server.Start();
        }

        public void UpdateServerList(IServerListData listData)
        {
            listData.ThrownOnArgNull(nameof(listData));

            _server.ServerListData = listData.GetString();
        }

        public void Dispose()
        {
            if (_server != null)
            {
                _server.WorkerCancelToken.Cancel();
                _server = null;
            }
        }
    }
}