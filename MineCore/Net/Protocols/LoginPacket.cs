using MineCore.Net.Impl;

namespace MineCore.Net.Protocols
{
    public class LoginPacket : DataPacket
    {
        public override byte PacketId => MineCraftProtocol.LOGIN_PACKET;

        public int Protocol { get; set; }

        public override void EncodePayload()
        {
            WriteInt(Protocol);
        }

        public override void DecodePayload()
        {
            Protocol = ReadInt();
        }
    }
}