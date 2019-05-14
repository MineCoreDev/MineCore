using System;
using System.IO.Compression;
using System.Net;
using BinaryIO;
using BinaryIO.Compression;
using MineCore.Configs;
using MineCore.Data;
using MineCore.Net;
using MineCore.Net.Protocols;
using MineCore.Net.Protocols.Defaults;
using MineCore.Utils;
using NLog;
using RakDotNet.Minecraft;
using RakDotNet.Minecraft.Packets;
using RakDotNet.Protocols.Packets.MessagePackets;

namespace MineCore.Entities.Impl
{
    public partial class ServerPlayer : Entity, IServerPlayer
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public IMineCraftProtocol Protocol { get; private set; }
        public IServerListData ServerListData { get; private set; }
        public MinecraftPeer ClientPeer { get; private set; }
        public IServerPlatformConfig ServerConfig { get; private set; }

        public ILoginData LoginData { get; private set; }
        public IClientData ClientData { get; private set; }
        public bool IsEncrypt { get; private set; }

        public ServerPlayer(MinecraftPeer peer, IServerNetworkManager networkManager)
        {
            Protocol = networkManager.Protocol;
            ServerListData = networkManager.ServerListData;
            ServerConfig = networkManager.ServerConfig;
            ClientPeer = peer;
        }

        public void SendDataPacket(DataPacket packet, bool needAck = false,
            Reliability reliability = Reliability.Reliable,
            CompressionLevel compressionLevel = CompressionLevel.Fastest)
        {
            packet.EncodeHeader();
            packet.EncodePayload();

            NetworkStream stream = new NetworkStream();
            stream.WriteVarInt((int) packet.Length);
            stream.WriteBytes(packet.GetBuffer());
            byte[] payload = CompressionManager.CompressionZlib(stream, compressionLevel, true);

            BatchPacket batch = new BatchPacket();
            batch.Payload = payload;
            batch.EndPoint = ClientPeer.PeerEndPoint;

            ClientPeer.SendEncapsulatedPacket(batch, reliability, packet.Channel);
        }

        public void Close(string message)
        {
            DisconnectPacket disconnectPacket = new DisconnectPacket();
            disconnectPacket.Message = message;

            SendDataPacket(disconnectPacket);
        }

        public void Dispose()
        {
        }
    }
}