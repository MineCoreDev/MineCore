using BinaryIO;
using MineCore.Data;
using MineCore.Net.Impl;

namespace MineCore.Net.Protocols.Defaults
{
    public class ResourcePacksInfoPacket : DataPacket
    {
        public override byte PacketId => MineCraftProtocol.RESOURCE_PACKS_INFO_PACKET;

        public bool MustAccept { get; set; } = false;
        public bool HasScripts { get; set; } = false;
        public IResourcePack[] BehaviourPackEntries { get; set; } = new IResourcePack[0];
        public IResourcePack[] ResourcePackEntries { get; set; } = new IResourcePack[0];

        public override void EncodePayload()
        {
            WriteBoolean(MustAccept);
            WriteBoolean(HasScripts);

            WriteResourcePack(BehaviourPackEntries);
            WriteResourcePack(ResourcePackEntries);
        }

        public override void DecodePayload()
        {
        }

        public void WriteResourcePack(IResourcePack[] entries)
        {
            WriteUShort((ushort) entries.Length, ByteOrder.Little);
            foreach (IResourcePack entry in entries)
            {
                WriteString(entry.PackId);
                WriteString(entry.PackVersion);
                WriteULong(entry.PackSize, ByteOrder.Little);
                WriteString(entry.EncryptionKey);
                WriteString(entry.SubPackName);
                WriteString(entry.ContentIdentity);
                WriteBoolean(entry.PackFlag);
            }
        }
    }
}