using System;
using System.IO.Compression;
using System.Net;
using BinaryIO;
using BinaryIO.Compression;
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
    public class Player : Entity, IPlayer
    {
        public MinecraftPeer ClientPeer { get; }

        public Player(MinecraftPeer peer)
        {
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

        public void HandleDataPacket(DataPacket packet)
        {
            packet.ThrownOnArgNull(nameof(packet));

            if (packet is LoginPacket loginPacket)
            {
                PlayStatusPacket pk = new PlayStatusPacket();
                pk.Status = PlayStatusPacket.LOGIN_FAILED_SERVER;

                SendDataPacket(pk, reliability: Reliability.Unreliable);
            }
        }

        public void Dispose()
        {
        }
    }
}