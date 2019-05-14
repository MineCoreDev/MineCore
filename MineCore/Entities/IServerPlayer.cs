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
    public interface IServerPlayer : IHuman, IDataPacketHandler, IDisposable
    {
        IMineCraftProtocol Protocol { get; }
        IServerListData ServerListData { get; }
        MinecraftPeer ClientPeer { get; }
        IServerPlatformConfig ServerConfig { get; }

        ILoginData LoginData { get; }
        IClientData ClientData { get; }

        void SendDataPacket(DataPacket packet, bool needAck = false, Reliability reliability = Reliability.Reliable,
            CompressionLevel compressionLevel = CompressionLevel.Fastest);
    }
}