using System;
using MineCore.Net.Protocols;
using Optional;

namespace MineCore.Net
{
    public interface IMineCraftProtocol
    {
        int ProtocolNumber { get; }

        string Version { get; }

        void RegisterDataPacket(int id, Type packetType, bool compile = false);
        void Compile();
        void CompileFromId(int id);
        Option<DataPacket> GetDefinedPacket(byte id);
    }
}