using System;
using System.Collections.Concurrent;
using System.Net;
using BinaryIO;
using BinaryIO.Compression;
using MineCore.Entities;
using MineCore.Entities.Impl;
using MineCore.Net.Protocols;
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
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private MinecraftServer _server;

        private ConcurrentDictionary<IPEndPoint, IPlayer> _players = new ConcurrentDictionary<IPEndPoint, IPlayer>();

        public IMineCraftProtocol Protocol { get; private set; }
        public IPEndPoint ServerEndPoint { get; private set; }

        public ServerNetworkManager(IPEndPoint endPoint, IMineCraftProtocol protocol, IServerListData listData)
        {
            endPoint.ThrownOnArgNull(nameof(endPoint));
            listData.ThrownOnArgNull(nameof(listData));

            ServerEndPoint = endPoint;

            Protocol = protocol;

            RakDotNet.Utils.Logger.PrintCallBack = log => _logger.Debug(log.Message);

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
            IPlayer player = new Player(Protocol, e.Peer);
            e.Peer.HandleBatchPacket = HandleBatchPacket;
            _players.TryAdd(e.Peer.PeerEndPoint, player);
        }

        private void Server_DisconnectPeerEvent(object sender, ServerDisconnectPeerEventArgs e)
        {
            _players.TryRemove(e.Peer.PeerEndPoint, out IPlayer player);
            player.Dispose();
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

                    _players.TryGetValue(packet.EndPoint, out IPlayer player);
                    player?.HandleDataPacket(pk);
                }
            }
        }
    }
}