using System.Collections.Concurrent;
using System.Net;
using MineCore.Entities;
using MineCore.Utils;
using NLog;
using RakDotNet.Event.RakNetServerEvents;
using RakDotNet.Minecraft;
using RakDotNet.Minecraft.Event.MineCraftServerEvents;
using RakDotNet.Minecraft.Packets;

namespace MineCore.Net.Impl
{
    public class ServerNetworkManager : IServerNetworkManager
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private MinecraftServer _server;

        private ConcurrentDictionary<IPEndPoint, IPlayer> _players = new ConcurrentDictionary<IPEndPoint, IPlayer>();

        public IPEndPoint ServerEndPoint { get; private set; }

        public ServerNetworkManager(IPEndPoint endPoint, IServerListData listData)
        {
            endPoint.ThrownOnArgNull(nameof(endPoint));
            listData.ThrownOnArgNull(nameof(listData));

            ServerEndPoint = endPoint;

            _server = new MinecraftServer(endPoint);
            _server.ConnectPeerEvent += Server_ConnectPeerEvent;
            _server.DisconnectPeerEvent += Server_DisconnectPeerEvent;
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

        private void Server_DisconnectPeerEvent(object sender, ServerDisconnectPeerEventArgs e)
        {
        }

        private void HandleBatchPacket(BatchPacket packet)
        {
            _logger.Info(packet.Payload.Length);
        }
    }
}