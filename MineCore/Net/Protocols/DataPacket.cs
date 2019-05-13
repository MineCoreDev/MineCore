using BinaryIO;

namespace MineCore.Net.Protocols
{
    public abstract class DataPacket : NetworkStream
    {
        public abstract byte PacketId { get; }
        public virtual byte Channel { get; }

        public virtual void EncodeHeader()
        {
            WriteByte(PacketId);
        }

        public abstract void EncodePayload();

        public virtual void DecodeHeader()
        {
            ReadByte();
        }

        public abstract void DecodePayload();
    }
}