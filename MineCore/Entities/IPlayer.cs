using System;
using System.IO.Compression;
using System.Net;
using BinaryIO.Compression;
using MineCore.Net;
using MineCore.Net.Protocols;
using RakDotNet.Minecraft;
using RakDotNet.Protocols.Packets.MessagePackets;

namespace MineCore.Entities
{
    public interface IPlayer : IHuman, IDataPacketHandler, IDisposable
    {
        MinecraftPeer ClientPeer { get; }

        void SendDataPacket(DataPacket packet, bool needAck = false, Reliability reliability = Reliability.Reliable,
            CompressionLevel compressionLevel = CompressionLevel.Fastest);
    }
}