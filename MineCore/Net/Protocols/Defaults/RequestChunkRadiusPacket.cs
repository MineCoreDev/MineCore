using MineCore.Net.Impl;

namespace MineCore.Net.Protocols.Defaults
{
    public class RequestChunkRadiusPacket : DataPacket
    {
        public override byte PacketId => MineCraftProtocol.REQUEST_CHUNK_RADIUS_PACKET;

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