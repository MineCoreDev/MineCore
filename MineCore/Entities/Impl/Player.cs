using System;
using System.IO.Compression;
using System.Net;
using BinaryIO;
using BinaryIO.Compression;
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
    public partial class Player : Entity, IPlayer
    {
        public IMineCraftProtocol Protocol { get; }
        public MinecraftPeer ClientPeer { get; }

        public ILoginData LoginData { get; private set; }
        public IClientData ClientData { get; private set; }

        public Player(IMineCraftProtocol protocol, MinecraftPeer peer)
        {
            Protocol = protocol;
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

        public void Dispose()
        {
        }
    }
}