using System;
using System.Collections.Concurrent;
using System.Net;
using BinaryIO;
using BinaryIO.Compression;
using MineCore.Configs;
using MineCore.Entities;
using MineCore.Entities.Impl;
using MineCore.Net.Protocols;
using MineCore.Platforms;
using MineCore.Utils;
using NLog;
using Optional.Unsafe;
using RakDotNet.Event.RakNetServerEvents;
using RakDotNet.Minecraft;
using RakDotNet.Minecraft.Event.MineCraftServerEvents;
using RakDotNet.Minecraft.Packets;

namespace MineCore.Net.Impl
{
    public class ServerNetworkManager : IServerNetworkManager
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private MinecraftServer _server;

        private ConcurrentDictionary<IPEndPoint, IServerPlayer> _players =
            new ConcurrentDictionary<IPEndPoint, IServerPlayer>();

        public IServerPlatformConfig ServerConfig { get; private set; }
        public IMineCraftProtocol Protocol { get; private set; }
        public IServerListData ServerListData { get; private set; }
        public IPEndPoint ServerEndPoint { get; private set; }

        public ServerNetworkManager(IPEndPoint endPoint, IServerPlatform platform)
        {
            endPoint.ThrownOnArgNull(nameof(endPoint));
            platform.ThrownOnArgNull(nameof(platform));

            ServerEndPoint = endPoint;

            ServerConfig = platform.ServerConfig;
            Protocol = platform.Protocol;
            ServerListData = new ServerListData(Protocol);
            ServerListData.ServerName = ServerConfig.ServerName;
            ServerListData.MaxPlayer = ServerConfig.ServerMaxPlayer;

            RakDotNet.Utils.Logger.PrintCallBack = log => _logger.Debug(log.Message);

            _server = new MinecraftServer(endPoint);
            _server.ConnectPeerEvent += Server_ConnectPeerEvent;
            _server.DisconnectPeerEvent += Server_DisconnectPeerEvent;
            UpdateServerList();
        }

        public void Start()
        {
            _server.Start();
        }

        public void UpdateServerList()
        {
            _server.ServerListData = ServerListData.GetString();
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
            IServerPlayer serverPlayer = new ServerPlayer(e.Peer, this);
            e.Peer.HandleBatchPacket = HandleBatchPacket;
            _players.TryAdd(e.Peer.PeerEndPoint, serverPlayer);

            ServerListData.JoinedPlayer = _players.Count;
            UpdateServerList();
        }

        private void Server_DisconnectPeerEvent(object sender, ServerDisconnectPeerEventArgs e)
        {
            _players.TryRemove(e.Peer.PeerEndPoint, out IServerPlayer player);
            player.Dispose();

            ServerListData.JoinedPlayer = _players.Count;
            UpdateServerList();
        }

        private void HandleBatchPacket(BatchPacket packet)
        {
            byte[] data = CompressionManager.DecompressionZlib(new BinaryStream(packet.Payload), true);

            using (NetworkStream stream = new NetworkStream(data))
            {
                while (!stream.IsEndOfStream())
                {
                    int len = stream.ReadVarInt();
                    byte[] payload = stream.ReadBytes(len);
                    DataPacket pk = Protocol.GetDefinedPacket(payload[0]).ValueOrFailure();

                    pk.SetBuffer(payload);

                    pk.DecodeHeader();
                    pk.DecodePayload();

                    _players.TryGetValue(packet.EndPoint, out IServerPlayer player);
                    player?.HandleDataPacket(pk);
                }
            }
        }
    }
}