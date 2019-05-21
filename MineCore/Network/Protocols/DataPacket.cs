using BinaryIO;

namespace MineCore.Network.Protocols
{
    public abstract class DataPacket : NetworkStream
    {
        public abstract int PacketId { get; }

        public void EncodeHeader()
        {
            WriteSVarInt(PacketId);
            Encode();
        }

        public abstract void Encode();

        public void DecodeHeader()
        {
            ReadByte();
            Decode();
        }

        public abstract void Decode();
    }
}