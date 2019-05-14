using MineCore.Net.Impl;

namespace MineCore.Net.Protocols.Defaults
{
    public class ResourcePackClientResponsePacket : DataPacket
    {
        public const byte STATUS_REFUSED = 1;
        public const byte STATUS_SEND_PACKS = 2;
        public const byte STATUS_HAVE_ALL_PACKS = 3;
        public const byte STATUS_COMPLETED = 4;
        public override byte PacketId => MineCraftProtocol.RESOURCE_PACK_CLIENT_RESPONSE_PACKET;

        public byte ResponseStatus { get; set; }
        public string[] PackIds { get; set; } = new string[0];

        public override void EncodePayload()
        {
            WriteByte(ResponseStatus);
            WriteUShort((ushort) PackIds.Length);
            foreach (string packId in PackIds)
            {
                WriteString(packId);
            }
        }

        public override void DecodePayload()
        {
            ResponseStatus = ReadByte();
            ushort len = ReadUShort();
            PackIds = new string[len];
            for (int i = 0; i < len; i++)
                PackIds[i] = ReadString();
        }
    }
}