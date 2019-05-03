using System;
using System.Net;
using MineCore.Net;
using MineCore.Utils;
using NLog;
using RakDotNet.Minecraft;
using RakDotNet.Minecraft.Event.MineCraftServerEvents;
using RakDotNet.Minecraft.Packets;

namespace MineCore.Impl.Net
{
    public class ServerNetworkManager : IServerNetworkManager
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private MinecraftServer _server;

        public ServerNetworkManager(IPEndPoint endPoint, IServerListData listData)
        {
            endPoint.ThrownOnArgNull(nameof(endPoint));
            listData.ThrownOnArgNull(nameof(listData));

            _server = new MinecraftServer(endPoint);
            _server.ConnectPeerEvent += Server_ConnectPeerEvent;
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

        private void Server_ConnectPeerEvent(object sender, MineCraftServerConnectPeerEventArgs e)
        {
            e.Peer.HandleBatchPacket = HandleBatchPacket;
        }

        private void HandleBatchPacket(BatchPacket packet)
        {
            _logger.Info(packet.Payload.Length);
        }
    }
}