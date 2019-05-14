using System;
using System.IO.Compression;
using System.Net;
using BinaryIO.Compression;
using MineCore.Configs;
using MineCore.Data;
using MineCore.Net;
using MineCore.Net.Protocols;
using RakDotNet.Minecraft;
using RakDotNet.Protocols.Packets.MessagePackets;

namespace MineCore.Entities
{
    public interface IServerPlayer : IPlayer, IDataPacketHandler
    {
        IServerListData ServerListData { get; }
        MinecraftPeer ClientPeer { get; }
        IServerPlatformConfig ServerConfig { get; }

        void SendDataPacket(DataPacket packet, bool needAck = false, Reliability reliability = Reliability.Reliable,
            CompressionLevel compressionLevel = CompressionLevel.Fastest);
    }
}