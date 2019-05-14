using MineCore.Data;
using MineCore.Net.Impl;

namespace MineCore.Net.Protocols.Defaults
{
    public class ResourcePackStackPacket : DataPacket
    {
        public override byte PacketId => MineCraftProtocol.RESOURCE_PACK_STACK_PACKET;

        public bool MustAccept { get; set; } = false;
        public IResourcePack[] BehaviourPackEntries { get; set; } = new IResourcePack[0];
        public IResourcePack[] ResourcePackEntries { get; set; } = new IResourcePack[0];
        public bool IsExperimental { get; set; } = false;

        public override void EncodePayload()
        {
            WriteBoolean(MustAccept);

            WriteResourcePack(BehaviourPackEntries);
            WriteResourcePack(ResourcePackEntries);

            WriteBoolean(IsExperimental);
        }

        public override void DecodePayload()
        {
            throw new System.NotImplementedException();
        }

        public void WriteResourcePack(IResourcePack[] resourcePacks)
        {
            WriteUVarInt((uint) resourcePacks.Length);
            foreach (IResourcePack entry in resourcePacks)
            {
                WriteString(entry.PackId);
                WriteString(entry.PackVersion);
                WriteString(entry.SubPackName);
            }
        }
    }
}