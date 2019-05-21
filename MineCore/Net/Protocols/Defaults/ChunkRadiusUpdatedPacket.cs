using MineCore.Net.Impl;

namespace MineCore.Net.Protocols.Defaults
{
    public class ChunkRadiusUpdatedPacket : DataPacket
    {
        public override byte PacketId => MineCraftProtocol.CHUNK_RADIUS_UPDATED_PACKET;

        public int Radius { get; set; }

        public override void EncodePayload()
        {
            WriteSVarInt(Radius);
        }

        public override void DecodePayload()
        {
            Radius = ReadSVarInt();
        }
    }
}